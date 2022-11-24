using Sistema.DUE.Web.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Cargill.DUE.Web.Models;
using Cargill.DUE.Web.Services;
using System.Web.UI;
using Cargill.DUE.Web.Models.SERPRO;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using Sistema.DUE.Web.Models;

namespace Sistema.DUE.Web
{
    public partial class BaixarNotasSerpro : System.Web.UI.Page
    {

        private ConsultaSerpro _consultaSerpro = new ConsultaSerpro();
        public List<string> consultaAverbacoes = new List<string>();
        private readonly TotalConsultaSerproDAO _totalDeConsultasREalizadas = new TotalConsultaSerproDAO();
        private ConsultarAverbacoes _consultarAverbacoes = new ConsultarAverbacoes();


        protected void Page_Load(object sender, EventArgs e)
        {
            //consultaAverbacoes = new List<ResultadoConsultaNFE>();
            var _totalConsultas = _totalDeConsultasREalizadas.ObterTotalDeConsultas();
            qtde_consultas.Text = _totalConsultas.ConsultasRealizadas.ToString();
        }

        override public void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("Default.aspx"));
        }

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            if (this.txtUpload.PostedFile == null)
                ModelState.AddModelError(string.Empty, "Nenhum arquivo informado");

            if (this.txtUpload.PostedFile.ContentLength == 0)
                ModelState.AddModelError(string.Empty, "Arquivo inválido");

            if (!this.txtUpload.PostedFile.FileName.ToUpper().EndsWith("TXT") && !this.txtUpload.PostedFile.FileName.ToUpper().EndsWith("CSV"))
                ModelState.AddModelError(string.Empty, "É permitido apenas importação de arquivos .txt");

            if (!ModelState.IsValid)
                return;

            if (!UploadArquivo(this.txtUpload))
            {
                throw new Exception("O arquivo não pode ser processado. Certifique-se que já não esteja aberto em outro programa");
            }

            var nfe = ProcessarArquivoTxt(this.txtUpload.PostedFile.InputStream, ";");
            List<ConsultaSerproView> consultaSerproView = new List<ConsultaSerproView>();
            bool local = this.searchInDataBase.Checked;
            if (local)
            {
                List<string> listChaves = new List<string>();
                listChaves = nfe.Distinct().ToList();
                //
                consultaAverbacoes = _totalDeConsultasREalizadas.DownloadXmlFile(listChaves);
                List<XmlDocument> filesXml = new List<XmlDocument>();
                byte[] compressedBytes;
                string fileNameZip = "Consulta" + DateTime.Now.ToString("yyyy-MM-dd hhmmss") + ".zip";


                for (int i = 0; i < consultaAverbacoes.Count(); i++)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(consultaAverbacoes[i]);
                    filesXml.Add(xmlDoc);
                }

                CreateZipFileContent(consultaAverbacoes);
                DeleteFilesXml();

                this.btnGerarExcel.Visible = nfe.Count > 0;
                this.qtde_consultas.Text = _totalDeConsultasREalizadas.ObterTotalDeConsultas().ConsultasRealizadas.ToString();
            }
            else
            {
                //consultaAverbacoes = ConsultarAverbacoesNfe(nfe);
                //
                var consultaAverbacoesXml = _consultarAverbacoes.ConsultarAverbacoesXmlNota(nfe);
                //ponto de alteracao
                for (int i = 0; i < consultaAverbacoesXml.Count(); i++)
                {
                    if (consultaAverbacoesXml[i].Eventos.Count() > 0)
                    {
                        var list = new List<EventosNfe>();


                        for (int j = 0; j < consultaAverbacoesXml[i].Eventos.Count(); j++)
                        {
                            string numeroItem = string.Empty;
                            numeroItem = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemNfe;
                            //
                            consultaSerproView.Add(new ConsultaSerproView()
                            {
                                ChaveNfe = consultaAverbacoesXml[i].ChaveNfe,
                                ArquivoXml = consultaAverbacoesXml[i].ArquivoXml,
                                UnidadeTributavel = consultaAverbacoesXml[i].Produtos.Where(x => x.NItem == numeroItem).FirstOrDefault().UnidadeTributavel,
                                QtdeTributavel = consultaAverbacoesXml[i].Produtos.Where(x => x.NItem == numeroItem).FirstOrDefault().QtdeTributavel,
                                ItemNfe = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemNfe,
                                DataDoEmbarque = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDoEmbarque,
                                DataAverbacao = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDaAverbacao,
                                QtdeAverbada = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.QtdeAverbada,
                                Due = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.Due,
                                ItemDue = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemDue,
                                Descricao = consultaAverbacoesXml[i].Eventos[j].Descricao,
                            });

                        }
                    }
                    else
                    {
                        if (consultaAverbacoesXml[i].Produtos.Count() > 0)
                        {
                            for (int p = 0; p < consultaAverbacoesXml[i].Produtos.Count(); p++)
                            {
                                consultaSerproView.Add(new ConsultaSerproView()
                                {
                                    ChaveNfe = consultaAverbacoesXml[i].ChaveNfe,
                                    ArquivoXml = consultaAverbacoesXml[i].ArquivoXml,
                                    UnidadeTributavel = consultaAverbacoesXml[i].Produtos?[p].UnidadeTributavel,
                                    QtdeTributavel = consultaAverbacoesXml[i].Produtos?[p].QtdeTributavel,
                                    Descricao = "Nota sem averbação"
                                });
                            }
                        }
                        else
                        {
                            consultaSerproView.Add(new ConsultaSerproView()
                            {
                                ChaveNfe = consultaAverbacoesXml[i].ChaveNfe,
                                ArquivoXml = consultaAverbacoesXml[i].ArquivoXml,
                                Descricao = "Erro ao tentar montar a nota em tela"
                            });
                        }
                    }
                }
                //gravando dados na tabela
                _consultarAverbacoes.gravarNotaNaBase(consultaSerproView);
                //buscando dados na tabela
                var consultas = _consultarAverbacoes.BuscarConsultasNaBase(consultaSerproView);
                List<string> listChaves = new List<string>();
                listChaves = nfe.Distinct().ToList();
                //
                consultaAverbacoes = _totalDeConsultasREalizadas.DownloadXmlFile(listChaves);
                //CreateZipFileContent(consultaAverbacoes);
                List<XmlDocument> filesXml = new List<XmlDocument>();
                byte[] compressedBytes;
                string fileNameZip = "Consulta" + DateTime.Now.ToString("yyyy-MM-dd hhmmss") + ".zip";


                for (int i = 0; i < consultaAverbacoes.Count(); i++)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(consultaAverbacoes[i]);
                    filesXml.Add(xmlDoc);
                }

                CreateZipFileContent(consultaAverbacoes);
                DeleteFilesXml();



                this.btnGerarExcel.Visible = consultas.Count > 0;
                this.qtde_consultas.Text = _totalDeConsultasREalizadas.ObterTotalDeConsultas().ConsultasRealizadas.ToString();
            }


        }

        private void DeleteFilesXml()
        {
            string path = Server.MapPath("Uploads\\");
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fi = di.GetFiles("*.xml");
            foreach (FileInfo filetemp in fi)
            {
                filetemp.Delete();
            }
        }

        public void CreateZipFileContent(List<string> consultaAverbacoes)
        {
            string path = Server.MapPath("Uploads\\");
            string fileName = "consulta.zip";
            foreach (var file in consultaAverbacoes)
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(file);

                XmlNode nodeName = xml.DocumentElement;

                File.WriteAllText(path + nodeName.SelectSingleNode("//nfeProc/protNFe/infProt/chNFe").InnerXml + ".xml", xml.InnerXml);
            }

            try
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=" + "consulta" + ".zip");
                Response.ContentType = "application/zip";
                using (var zipStream = new ZipOutputStream(Response.OutputStream))
                {
                    // note: this does not recurse directories! 
                    String[] filenames = Directory.GetFiles(path, "*.xml");
                    foreach (String filename in filenames)
                    {
                        byte[] fileBytes = System.IO.File.ReadAllBytes(filename);
                        var fileEntry = new ZipEntry(Path.GetFileName(filename))
                        {
                            Size = fileBytes.Length
                        };

                        zipStream.PutNextEntry(fileEntry);
                        zipStream.Write(fileBytes, 0, fileBytes.Length);
                    }

                    zipStream.Flush();
                    zipStream.Close();
                }
            }
            catch (System.Exception ex1)
            {
                System.Console.Error.WriteLine("exception: " + ex1);
            }
        }

        private List<string> ConsultarAverbacoesNfe(List<string> nfe)
        {
            List<string> consultas = new List<string>();

            if (Session["SerproToken"] == null)
                Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;

            string token = (string)(Session["SerproToken"]);

            for (int i = 0; i < nfe.Count(); i++)
            {
                string result = _consultaSerpro.GetXml(token, nfe[i]).Result;
                if (result == null)
                {
                    Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;
                    token = (string)(Session["SerproToken"]);
                    result = _consultaSerpro.GetXml(token, nfe[i]).Result;
                }

                consultas.Add(result);

            }

            return consultas;
        }

        private ResultadoConsultaNFE ResultadoDaCOnsulta(ConsultaAverbacoesNFE consulta, string chavePesquisada)
        {
            ResultadoConsultaNFE resultadoConsulta = null;
            List<EventosNfe> eventosNfe = new List<EventosNfe>();
            List<ProdutosNfe> produtosNfe = new List<ProdutosNfe>();

            foreach (var item in consulta.procEventoNFe.Where(x => x.evento.infEvento.detEvento.itensAverbados != null))
            {
                eventosNfe.Add(new EventosNfe()
                {
                    Descricao = item.evento.infEvento.detEvento.descEvento,
                    Evento = new EventoNFE()
                    {
                        InfoEvento = new InfoEventoNFE()
                        {
                            DetalheEvento = new DetalheEventoNFE()
                            {
                                ItensAverbados = new ItensAverbados()
                                {
                                    //UnidadeTributavel = item.,
                                    //QtdeTributavel = result.nfeProc.NFe.infNFe.det.FirstOrDefault().prod.qTrib,
                                    ItemNfe = item.evento.infEvento.detEvento.itensAverbados?.nItem,
                                    DataDoEmbarque = item.evento.infEvento.detEvento.itensAverbados?.dhEmbarque,
                                    DataDaAverbacao = item.evento.infEvento.detEvento.itensAverbados?.dhAverbacao,
                                    QtdeAverbada = item.evento.infEvento.detEvento.itensAverbados?.qItem,
                                    Due = item.evento.infEvento.detEvento.itensAverbados?.nDue,
                                    ItemDue = item.evento.infEvento.detEvento.itensAverbados?.nItemDue
                                }
                            }
                        }
                    }
                });
            }

            foreach (var item in consulta.nfeProc.NFe.infNFe.det)
            {
                produtosNfe.Add(new ProdutosNfe()
                {
                    UnidadeTributavel = item.prod.uTrib,
                    QtdeTributavel = item.prod.qTrib
                });
            }

            resultadoConsulta = new ResultadoConsultaNFE()
            {
                ChaveNfe = chavePesquisada,
                Eventos = eventosNfe,
                Produtos = produtosNfe
            };

            return resultadoConsulta;
        }

        private List<string> ProcessarArquivoTxt(Stream arquivo, string delimitador)
        {
            List<string> dues = new List<string>();

            using (StreamReader reader = new StreamReader(arquivo))
            {
                string linha = string.Empty;

                try
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(linha))
                        {
                            dues.Add(linha);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return dues;
            }
        }

        private bool UploadArquivo(FileUpload arquivo)
        {
            string nomeArquivo = Path.Combine(Server.MapPath("Uploads"), this.txtUpload.PostedFile.FileName);

            try
            {
                arquivo.SaveAs(nomeArquivo);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool DeletarArquivo(FileUpload arquivo)
        {
            try
            {
                string caminho = Path.Combine(Server.MapPath("Uploads"), this.txtUpload.PostedFile.FileName);

                if (System.IO.File.Exists(caminho))
                    System.IO.File.Delete(caminho);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnGerarExcel_Click(object sender, EventArgs e)
        {
        }
    }
}