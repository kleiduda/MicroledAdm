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
using NUnit.Framework;
using System.Web.Services.Description;
using Ionic.Zip;
using System.IO.Compression;
using System.Xml;
using System.Security.AccessControl;
using System.Text;

namespace Sistema.DUE.Web
{
    public partial class BaixarNotasSerpro : System.Web.UI.Page
    {

        private ConsultaSerpro _consultaSerpro = new ConsultaSerpro();
        public List<string> consultaAverbacoes = new List<string>();
        private readonly TotalConsultaSerproDAO _totalDeConsultasREalizadas = new TotalConsultaSerproDAO();


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
            consultaAverbacoes = ConsultarAverbacoesNfe(nfe);

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
            


            this.btnGerarExcel.Visible = nfe.Count > 0;
            this.qtde_consultas.Text = _totalDeConsultasREalizadas.ObterTotalDeConsultas().ConsultasRealizadas.ToString();

        }

        public void CreateZipFileContent(List<string> consultaAverbacoes)
        {

            string path = Server.MapPath("Uploads\\");
            using (var stream = File.OpenWrite(path))
            {
                using (ZipArchive zip = new ZipArchive(stream, ZipArchiveMode.Create))
                {
                    foreach (string file in consultaAverbacoes)
                    {
                        zip.CreateEntryFromFile(file, Path.GetFileName(path), CompressionLevel.Optimal);
                    }
                }
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