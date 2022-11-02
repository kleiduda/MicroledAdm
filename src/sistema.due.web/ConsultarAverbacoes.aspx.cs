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

namespace Sistema.DUE.Web
{
    public partial class ConsultarAverbacoes : System.Web.UI.Page
    {

        private ConsultaSerpro _consultaSerpro = new ConsultaSerpro();
        public List<ResultadoConsultaNFE> consultaAverbacoes = new List<ResultadoConsultaNFE>();
        public List<ResultadoConsultaNFE> consultaAverbacoesXml = new List<ResultadoConsultaNFE>();
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
            ConsultaSerproView consultaView = new ConsultaSerproView();
            var consultaAverbacoesXml = ConsultarAverbacoesXmlNota(dues);
            //consultaAverbacoes = ConsultarAverbacoesNfe(dues);
            for (int i = 0; i < consultaAverbacoesXml.Count(); i++)
            {
                if (consultaAverbacoesXml[i].Eventos.Count() > 0)
                {
                    for (int j = 0; j < consultaAverbacoesXml[i].Eventos.Count(); j++)
                    {
                        consultaSerproView.Add(new ConsultaSerproView()
                        {
                            ChaveNfe = consultaAverbacoesXml[i].ChaveNfe,
                            UnidadeTributavel = consultaAverbacoesXml[i].Produtos.Where(x => x.NItem == 
                            consultaAverbacoesXml[i].Eventos?[j].Evento?.InfoEvento?.DetalheEvento?.ItensAverbados?.ItemDue).Select(x => x.UnidadeTributavel).DefaultIfEmpty("Unknown").FirstOrDefault(),
                            QtdeTributavel = consultaAverbacoesXml[i].Produtos.Where(x => x.NItem == 
                            consultaAverbacoesXml[i].Eventos[j]?.Evento?.InfoEvento?.DetalheEvento?.ItensAverbados?.ItemDue).Select(x => x.QtdeTributavel).DefaultIfEmpty(0).FirstOrDefault(),
                            ItemNfe = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemNfe,
                            DataDoEmbarque = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDoEmbarque,
                            DataDaAverbacao = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.DataDaAverbacao,
                            QtdeAverbada = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.QtdeAverbada,
                            Due = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.Due,
                            ItemDue = consultaAverbacoesXml[i].Eventos[j].Evento.InfoEvento.DetalheEvento.ItensAverbados.ItemDue,
                            Descricao = consultaAverbacoesXml[i].Eventos[j].Descricao,
                        });

                    }
                }
                else
                {
                    consultaSerproView.Add(new ConsultaSerproView()
                    {
                        ChaveNfe = consultaAverbacoesXml[i].ChaveNfe,
                        Descricao = "Erro ao tentar montar a nota em tela"
                    });
                }

                //if (consultaAverbacoes[i].Produtos != null)
                //{

                //    for (int l = 0; l < consultaAverbacoes[i].Produtos?.Count(); l++)
                //    {
                //        consultaSerproView.Add(new ConsultaSerproView()
                //        {
                //            ChaveNfe = consultaAverbacoesXml[i].ChaveNfe,
                //            UnidadeTributavel = consultaAverbacoesXml[i].Produtos?[l].UnidadeTributavel,
                //            QtdeTributavel = consultaAverbacoesXml[i].Produtos?[l].QtdeTributavel,
                //            Descricao = "Nota sem averbação"
                //        });
                //    }
                //}
                //else
                //{
                //    consultaSerproView.Add(new ConsultaSerproView()
                //    {
                //        ChaveNfe = consultaAverbacoesXml[i].ChaveNfe,
                //        Descricao = "Erro ao tentar montar a nota em tela"
                //    });
                //}
                

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
        private List<ResultadoConsultaNFE> ConsultarAverbacoesXmlNota(List<string> dues)
        {
            //consultar API
            //pegar o token de acesso
            Envelope envelope = null;
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
                var result = _consultaSerpro.GetXml(token, dues[i]).Result;
                if (result == null)
                {
                    Session["SerproToken"] = _consultaSerpro.GenerateAccessToken().Result;
                    token = (string)(Session["SerproToken"]);
                    result = _consultaSerpro.GetXml(token, dues[i]).Result;
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

                chavePesquisada = dues[i];

                //guardar o retorno na classe
                if (result != null)
                {
                    consultas.Add(ResultadoDaCOnsultaXml(envelope, chavePesquisada));
                }

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
        private ResultadoConsultaNFE ResultadoDaCOnsultaXml(Envelope consulta, string chavePesquisada)
        {
            ResultadoConsultaNFE resultadoConsulta = null;
            List<EventosNfe> eventosNfe = new List<EventosNfe>();
            List<ProdutosNfe> produtosNfe = new List<ProdutosNfe>();

            if (consulta != null)

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
                                            ItemNfe = Convert.ToInt32(item.evento.infEvento.detEvento.itensAverbados?[i].nItem),
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
            Response.ContentType = "application/excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvNotasFiscais.GridLines = GridLines.Both;

            gvNotasFiscais.HeaderStyle.Font.Bold = true;
            foreach (GridViewRow row in gvNotasFiscais.Rows)
            {
                foreach ( TableCell cell in row.Cells )
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
    }
}