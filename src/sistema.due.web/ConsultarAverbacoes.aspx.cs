using Sistema.DUE.Web.DAO;
using Sistema.DUE.Web.Extensions;
using Sistema.DUE.Web.Models;
using Sistema.DUE.Web.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;
using Cargill.DUE.Web.Models;
using Cargill.DUE.Web.Services.Interfaces;
using Cargill.DUE.Web.Services;
using System.Threading.Tasks;
using System.Web.UI;
using Cargill.DUE.Web.Models.SERPRO;
using System.Net;

namespace Sistema.DUE.Web
{
    public partial class ConsultarAverbacoes : System.Web.UI.Page
    {

        private ConsultaSerpro _consultaSerpro = new ConsultaSerpro();
        public List<ResultadoConsultaNFE> consultaAverbacoes = new List<ResultadoConsultaNFE>();
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

            var dues = ProcessarArquivoTxt(this.txtUpload.PostedFile.InputStream, ";");
            List<ConsultaSerproView> consultaSerproView = new List<ConsultaSerproView>();
            consultaAverbacoes = ConsultarAverbacoesNfe(dues);
            for (int i = 0; i < consultaAverbacoes.Count(); i++)
            {
                if (consultaAverbacoes[i].Eventos.Count() > 0)
                {
                    for (int j = 0; j < consultaAverbacoes[i].Eventos.Count(); j++)
                    {
                        for (int l = 0; l < consultaAverbacoes[i].Produtos.Count(); l++)
                        {
                            consultaSerproView.Add(new ConsultaSerproView()
                            {
                                ChaveNfe = consultaAverbacoes[i].ChaveNfe,
                                UnidadeTributavel = consultaAverbacoes[i].Produtos[l].UnidadeTributavel,
                                QtdeTributavel = consultaAverbacoes[i].Produtos[l].QtdeTributavel,
                                ItemNfe = consultaAverbacoes[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemNfe,
                                DataDoEmbarque = consultaAverbacoes[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDoEmbarque,
                                DataDaAverbacao = consultaAverbacoes[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDaAverbacao,
                                QtdeAverbada = consultaAverbacoes[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.QtdeAverbada,
                                Due = consultaAverbacoes[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.Due,
                                ItemDue = consultaAverbacoes[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemDue,
                                Descricao = consultaAverbacoes[i].Eventos[j].Descricao,
                            });
                        }
                    }
                }
                else
                {
                    for (int l = 0; l < consultaAverbacoes[i].Produtos.Count(); l++)
                    {
                        consultaSerproView.Add(new ConsultaSerproView()
                        {
                            ChaveNfe = consultaAverbacoes[i].ChaveNfe,
                            UnidadeTributavel = consultaAverbacoes[i].Produtos[l].UnidadeTributavel,
                            QtdeTributavel = consultaAverbacoes[i].Produtos[l].QtdeTributavel,
                            Descricao = "Nota sem averbação"
                        });
                    }
                }
            }

            //populando em tela
            this.gvNotasFiscais.DataSource = consultaSerproView;
            this.gvNotasFiscais.DataBind();

            this.btnGerarExcel.Visible = dues.Count > 0;
            this.qtde_consultas.Text = _totalDeConsultasREalizadas.ObterTotalDeConsultas().ConsultasRealizadas.ToString();

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
                if (result == null)
                {
                    Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;
                    token = (string)(Session["SerproToken"]);
                    result = _consultaSerpro.GetNfe(token, dues[i]).Result;
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

        private ResultadoConsultaNFE ResultadoDaCOnsulta(ConsultaAverbacoesNFE consulta, string chavePesquisada)
        {
            ResultadoConsultaNFE resultadoConsulta = null;
            List<EventosNfe> eventosNfe = new List<EventosNfe>();
            List<ProdutosNfe> produtosNfe = new List<ProdutosNfe>();

            if(consulta.procEventoNFe != null)
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
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvNotasFiscais.GridLines = GridLines.Both;
            
            gvNotasFiscais.HeaderStyle.Font.Bold = true;
            
            gvNotasFiscais.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }
    }
}