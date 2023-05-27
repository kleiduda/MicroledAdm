using Sistema.DUE.Web.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Cargill.DUE.Web.Models;
using Cargill.DUE.Web.Services;
using System.Web.UI;
using Cargill.DUE.Web.Models.SERPRO;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;
using NUnit.Framework.Interfaces;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using System.Threading;
using System.Web.Services.Description;
using Ionic.Zip;

namespace Sistema.DUE.Web
{
    public partial class ConsultarAverbacoes : System.Web.UI.Page
    {

        private ConsultaSerpro _consultaSerpro = new ConsultaSerpro();
        public List<ResultadoConsultaNFE> consultaAverbacoes = new List<ResultadoConsultaNFE>();
        public List<ResultadoConsultaNFE> consultaAverbacoesXml = new List<ResultadoConsultaNFE>();
        private readonly TotalConsultaSerproDAO _totalDeConsultasREalizadas = new TotalConsultaSerproDAO();
        public List<ConsultaSerproView> consultas = new List<ConsultaSerproView>();
        public List<string> _consultaAverbacoes = new List<string>();
        public int totalConsultasValidas = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            //consultaAverbacoes = new List<ResultadoConsultaNFE>();
            var _totalConsultas = _totalDeConsultasREalizadas.ObterTotalDeConsultas();
            qtde_consultas.Text = _totalConsultas.ToString();
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

            var dues = ProcessarArquivoTxt(this.txtUpload.PostedFile.InputStream, ";");
            List<ConsultaSerproView> consultaSerproView = new List<ConsultaSerproView>();
            ConsultaSerproView consultaView = new ConsultaSerproView();
            //validando se a consulta 'e na base ou no SERPRO
            bool local = this.searchInDataBase.Checked;

            if (local)
            {
                List<string> _chavesNaoEncontradas = new List<string>();
                for (int i = 0; i < dues.Count(); i++)
                {
                    var _chaveConsultada = _totalDeConsultasREalizadas.ConsultaPriorizadaNaBase(dues[i]);
                    if (_chaveConsultada == null)
                    {
                        //montar a lista de string para consulta no serpro
                        _chavesNaoEncontradas.Add(dues[i]);
                    }
                }
                //
                if (_chavesNaoEncontradas.Count() > 0)
                {
                    //consulta no serpro
                    var totalConsultas = ConsultaChaveSerpro(_chavesNaoEncontradas);
                    _totalDeConsultasREalizadas.GravarConsultaRealizad(totalConsultas);
                }
                var totalConsultasRealizadas = dues.Count() - _chavesNaoEncontradas.Count();
                _totalDeConsultasREalizadas.GravarConsultaRealizad(totalConsultasRealizadas);
                consultas = _totalDeConsultasREalizadas.BuscarConsultasNaBase(dues);
                //populando em tela
                this.gvNotasFiscais.DataSource = consultas;
                this.gvNotasFiscais.DataBind();

                this.btnGerarExcel.Visible = dues.Count > 0;
                //this.btnDownloadZip.Visible = dues.Count > 0;
                this.qtde_consultas.Text = _totalDeConsultasREalizadas.ObterTotalDeConsultas().ToString();
            }
            else
            {
                var totalConsultasRealizadas = ConsultaChaveSerpro(dues);
                _totalDeConsultasREalizadas.GravarConsultaRealizad(totalConsultasRealizadas);
                consultas = _totalDeConsultasREalizadas.BuscarConsultasNaBase(dues);

                //populando em tela
                this.gvNotasFiscais.DataSource = consultas;
                this.gvNotasFiscais.DataBind();

                this.btnGerarExcel.Visible = dues.Count > 0;
                this.qtde_consultas.Text = _totalDeConsultasREalizadas.ObterTotalDeConsultas().ToString();

            }
        }

        public int ConsultaChaveSerpro(List<string> dues)
        {
            List<ConsultaSerproView> consultaSerproView = new List<ConsultaSerproView>();
            //passar uma due por vez
            for (int i = 0; i < dues.Count; i++)
            {
                var consultaAverbacoesXml = ConsultarAverbacoesXmlNota(dues[i]);
                //
                //ponto de alteracao

                if (consultaAverbacoesXml.Eventos.Count() > 0)
                {
                    var list = new List<EventosNfe>();

                    for (int j = 0; j < consultaAverbacoesXml.Eventos.Count(); j++)
                    {
                        string numeroItem = string.Empty;
                        numeroItem = consultaAverbacoesXml.Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemNfe;
                        //
                        consultaSerproView.Add(new ConsultaSerproView()
                        {
                            ChaveNfe = consultaAverbacoesXml.ChaveNfe,
                            ArquivoXml = consultaAverbacoesXml.ArquivoXml,
                            UnidadeTributavel = consultaAverbacoesXml.Produtos.Where(x => x.NItem == numeroItem).FirstOrDefault().UnidadeTributavel,
                            QtdeTributavel = consultaAverbacoesXml.Produtos.Where(x => x.NItem == numeroItem).FirstOrDefault().QtdeTributavel,
                            ItemNfe = consultaAverbacoesXml.Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemNfe,
                            DataDoEmbarque = consultaAverbacoesXml.Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDoEmbarque,
                            DataAverbacao = consultaAverbacoesXml.Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDaAverbacao,
                            QtdeAverbada = consultaAverbacoesXml.Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.QtdeAverbada,
                            Due = consultaAverbacoesXml.Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.Due,
                            ItemDue = consultaAverbacoesXml.Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemDue,
                            Descricao = consultaAverbacoesXml.Eventos[j].Descricao,
                        });

                    }
                }
                else
                {
                    if (consultaAverbacoesXml.Produtos.Count() > 0)
                    {
                        for (int p = 0; p < consultaAverbacoesXml.Produtos.Count(); p++)
                        {
                            consultaSerproView.Add(new ConsultaSerproView()
                            {
                                ChaveNfe = consultaAverbacoesXml.ChaveNfe,
                                ArquivoXml = consultaAverbacoesXml.ArquivoXml,
                                UnidadeTributavel = consultaAverbacoesXml.Produtos?[p].UnidadeTributavel,
                                QtdeTributavel = consultaAverbacoesXml.Produtos?[p].QtdeTributavel,
                                Descricao = "Nota sem averbação"
                            });
                        }
                    }
                    else
                    {
                        consultaSerproView.Add(new ConsultaSerproView()
                        {
                            ChaveNfe = consultaAverbacoesXml.ChaveNfe,
                            ArquivoXml = consultaAverbacoesXml.ArquivoXml,
                            Descricao = "Erro ao tentar montar a nota em tela"
                        });
                    }
                }

                //gravando dados na tabela
                totalConsultasValidas += 1;

                gravarNotaNaBase(consultaSerproView);
                consultaSerproView.Clear();

            }
            return totalConsultasValidas;
        }

        public void gravarNotaNaBase(List<ConsultaSerproView> consultaSerproView)
        {
            _totalDeConsultasREalizadas.GravarConsultaNaBase(consultaSerproView);
        }

        public List<ConsultaSerproView> BuscarConsultasNaBase(List<ConsultaSerproView> consultaSerproView)
        {
            List<string> chaves = new List<string>();

            for (int i = 0; i < consultaSerproView.Count(); i++)
            {
                chaves.Add(consultaSerproView[i].ChaveNfe);
            }
            return _totalDeConsultasREalizadas.BuscarConsultasNaBase(chaves);
        }


        private List<ResultadoConsultaNFE> ConsultarAverbacoesNfe(List<string> dues)
        {
            //consultar API
            //pegar o token de acesso
            List<ResultadoConsultaNFE> consultas = new List<ResultadoConsultaNFE>();
            List<EventosNfe> eventos = new List<EventosNfe>();
            List<ItensAverbados> itens = new List<ItensAverbados>();
            //_consultaSerpro = new ConsultaSerpro();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (Session["SerproToken"] == null)
                Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;

            string token = (string)(Session["SerproToken"]);

            string chavePesquisada = "";

            for (int i = 0; i < dues.Count(); i++)
            {
                var result = _consultaSerpro.GetNfe(token, dues[i]).Result;
                //var result = _consultaSerpro.GetXml(token, dues[i]).Result;
                if (result == null)
                {
                    Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;
                    token = (string)(Session["SerproToken"]);
                    //result = _consultaSerpro.GetNfe(token, dues[i]).Result;
                }

                chavePesquisada = dues[i];

                //guardar o retorno na classe
                if (result.nfeProc != null)
                {
                    consultas.Add(ResultadoDaCOnsulta(result, chavePesquisada));
                }
                else
                {
                    consultas.Add(new ResultadoConsultaNFE()
                    {
                        ChaveNfe = result.Error
                    });
                }

            }

            return consultas;
        }
        public ResultadoConsultaNFE ConsultarAverbacoesXmlNota(string due)
        {
            //consultar API
            //pegar o token de acesso
            Envelope envelope = null;
            ResultadoConsultaNFE consultas = new ResultadoConsultaNFE();
            List<EventosNfe> eventos = new List<EventosNfe>();
            List<ItensAverbados> itens = new List<ItensAverbados>();
            //_consultaSerpro = new ConsultaSerpro();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (Session["SerproToken"] == null)
                Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;

            string token = (string)(Session["SerproToken"]);

            string chavePesquisada = "";
            string arquivoXml = string.Empty;


            var result = _consultaSerpro.GetXml(token, due).Result;
            if (result == null)
            {
                Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;
                token = (string)(Session["SerproToken"]);
                result = _consultaSerpro.GetXml(token, due).Result;
            }

            if (result != null)
            {
                using (TextReader sr = new StringReader(result))
                {
                    var serializer = new XmlSerializer(typeof(Envelope));
                    try
                    {
                        envelope = (Envelope)serializer.Deserialize(sr);
                    }
                    catch (Exception ex)
                    {

                        if (ex.InnerException.Message != null)
                        {

                        }
                    }
                }
            }

            chavePesquisada = due;
            arquivoXml = result;


            //guardar o retorno na classe
            if (result != null)
            {
                consultas = ResultadoDaCOnsultaXml(envelope, chavePesquisada, arquivoXml);
            }



            return consultas;
        }

        private ResultadoConsultaNFE ResultadoDaCOnsulta(ConsultaAverbacoesNFE consulta, string chavePesquisada)
        {
            ResultadoConsultaNFE resultadoConsulta = null;
            List<EventosNfe> eventosNfe = new List<EventosNfe>();
            List<ProdutosNfe> produtosNfe = new List<ProdutosNfe>();

            if (consulta.procEventoNFe != null)
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
        private ResultadoConsultaNFE ResultadoDaCOnsultaXml(Envelope consulta, string chavePesquisada, string arquivoXml)
        {
            ResultadoConsultaNFE resultadoConsulta = null;
            List<EventosNfe> eventosNfe = new List<EventosNfe>();
            List<ProdutosNfe> produtosNfe = new List<ProdutosNfe>();

            if (consulta != null)
                if (consulta.Body.nfeConsultaNFeLogResult.retConsNFeLog.NFeLog.procEventoNFe != null)
                {
                    foreach (var item in consulta.Body.nfeConsultaNFeLogResult.retConsNFeLog.NFeLog.procEventoNFe.Where(x => x.evento.infEvento.detEvento.itensAverbados != null))
                    {
                        for (int i = 0; i < item.evento.infEvento.detEvento.itensAverbados.Count(); i++)
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
                                                ItemNfe = item.evento.infEvento.detEvento.itensAverbados?[i].nItem,
                                                DataDoEmbarque = item.evento.infEvento.detEvento.itensAverbados?[i].dhEmbarque,
                                                DataDaAverbacao = item.evento.infEvento.detEvento.itensAverbados?[i].dhAverbacao,
                                                QtdeAverbada = item.evento.infEvento.detEvento.itensAverbados?[i].qItem,
                                                Due = item.evento.infEvento.detEvento.itensAverbados?[i].nDue,
                                                ItemDue = item.evento.infEvento.detEvento.itensAverbados?[i].nItemDue
                                            }
                                        }
                                    }
                                }
                            });
                        }

                    }
                }

            if (consulta != null)
            {
                foreach (var item in consulta.Body.nfeConsultaNFeLogResult.retConsNFeLog.NFeLog.nfeProc.NFe.infNFe.det)
                {
                    produtosNfe.Add(new ProdutosNfe()
                    {
                        NItem = item.nItem,
                        UnidadeTributavel = item.prod.uTrib,
                        QtdeTributavel = item.prod.qTrib
                    });
                }
            }


            resultadoConsulta = new ResultadoConsultaNFE()
            {
                ChaveNfe = chavePesquisada,
                Eventos = eventosNfe,
                Produtos = produtosNfe,
                ArquivoXml = arquivoXml
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

        protected void btnGerarExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "UTF-8";
            string FileName = "consultaNfe" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvNotasFiscais.GridLines = GridLines.Both;

            gvNotasFiscais.HeaderStyle.Font.Bold = true;
            foreach (GridViewRow row in gvNotasFiscais.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.CssClass = "textmode";
                }
            }

            gvNotasFiscais.RenderControl(htmltextwrtter);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Write(strwritter.ToString());
            Response.End();
        }
        protected void btnDownloadZip_Click(object sender, EventArgs e)
        {
            //baixando o arquivo antes de excluir
            //string fileName = "consulta.zip";
            string caminhoArquivo = Server.MapPath("Uploads\\consulta.zip");

            //File to be downloaded.
            string fileName = "consulta.zip";

            //Path of the File to be downloaded.
            string filePath = Server.MapPath(string.Format("~/Uploads/{0}", fileName));

            //Content Type and Header.
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

            //Writing the File to Response Stream.
            Response.WriteFile(filePath);

            //Flushing the Response.
            Response.Flush();
            Response.End();

            //try
            //{
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + "consulta" + ".zip");
            //    Response.ContentType = "application/zip";
            //    using (var zipStream = new ZipOutputStream(Response.OutputStream))
            //    {
            //        // note: this does not recurse directories! 
            //        String[] filenames = Directory.GetFiles(path, "*.xml");
            //        foreach (String filename in filenames)
            //        {
            //            byte[] fileBytes = System.IO.File.ReadAllBytes(filename);
            //            var fileEntry = new ZipEntry(Path.GetFileName(filename))
            //            {
            //                Size = fileBytes.Length
            //            };

            //            zipStream.PutNextEntry(fileEntry);
            //            zipStream.Write(fileBytes, 0, fileBytes.Length);
            //        }

            //        zipStream.Flush();
            //        zipStream.Close();
            //    }
            //}
            //catch (System.Exception ex1)
            //{
            //    System.Console.Error.WriteLine("exception: " + ex1);
            //}

            //byte[] finalResult = File.ReadAllBytes(path + fileName);
            //if (File.Exists(path + fileName))
            //{
            //    File.Delete(path + fileName);
            //}
            //if (finalResult == null || !finalResult.Any())
            //{
            //    throw new Exception(String.Format("Nothing found"));

            //}


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
    }
}