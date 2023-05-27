using Cargill.DUE.Web.Models;
using Sistema.DUE.Web.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Cargill.DUE.Web
{
    public partial class EnviarXmlCct : System.Web.UI.Page
    {
        private readonly HttpResponseMessage response;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnValidaToken_Click(object sender, EventArgs e)
        {
            string file = "";
            string path = Server.MapPath("Uploads\\");

            if (txtUpload.HasFile)
            {
                String str = Server.HtmlEncode(txtUpload.FileName);
                String ext = Path.GetExtension(str);
                file = str;
                if ((ext == ".csv") || (ext == ".txt"))
                {
                    path += str;

                    txtUpload.SaveAs(path);
                }
                else
                {
                    lblContentResult.Visible = true;
                    lblContentResult.Text = "FORMATO DE ARQUIVO INVÁLIDO!!!";
                }
            }
            if (string.IsNullOrEmpty(file))
            {
                lblContentResult.Text = "Escolha um arquivo!!!";
            }
            else
            {
                //ValidateFile(file);
                List<string> convertXmlText = ConvertXmlFileAndSendNF(file);
                if (convertXmlText != null)
                {
                    List<RetornoXmlCCT> responseCct = new List<RetornoXmlCCT>();

                    try
                    {
                        for (int i = 0; i < convertXmlText.Count(); i++)
                        {
                            string xml = convertXmlText[i];
                            RetornoXmlCCT retornoXmlCCT = new RetornoXmlCCT();

                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(xml);

                            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                            namespaceManager.AddNamespace("ns", "http://www.pucomex.serpro.gov.br/cct");

                            XmlNode retornoServicoNode = xmlDoc.SelectSingleNode("/ns:retornoServico", namespaceManager);
                            if (retornoServicoNode != null)
                            {
                                // Aqui você pode acessar os elementos dentro de <retornoServico> conforme necessário
                                // Por exemplo:
                                XmlNodeList mensagensNodeList = retornoServicoNode.SelectNodes("ns:mensagens/ns:mensagem", namespaceManager);
                                foreach (XmlNode mensagemNode in mensagensNodeList)
                                {
                                    // Processar as mensagens dentro de <mensagens>
                                    string codigo = mensagemNode.SelectSingleNode("ns:codigo", namespaceManager)?.InnerText;
                                    string descricao = mensagemNode.SelectSingleNode("ns:descricao", namespaceManager)?.InnerText;
                                    // Faça o que for necessário com os valores obtidos
                                    retornoXmlCCT.message = descricao;
                                    retornoXmlCCT.code = codigo;
                                    retornoXmlCCT.status = "ERRO";
                                }

                            }
                            else
                            {
                                XmlNode errorNode = xmlDoc.SelectSingleNode("/error");
                                XmlNode statusNode = errorNode.SelectSingleNode("status");
                                if (statusNode != null && statusNode.InnerText == "200")
                                {
                                    retornoXmlCCT = new RetornoXmlCCT("Recepção efetuada com sucesso", "200", "CCTR-2");
                                }

                                if (statusNode != null && statusNode.InnerText == "422")
                                {
                                    XmlNode messageNode = errorNode.SelectSingleNode("message");
                                    XmlNode codeNode = errorNode.SelectSingleNode("code");
                                    if (messageNode.InnerText.Contains("Problemas encontrados:"))
                                    {
                                        XmlNode messageDetailErrorNode = errorNode.SelectSingleNode("detail").SelectSingleNode("error").SelectSingleNode("message");
                                        XmlNode statusDetailErrorNode = errorNode.SelectSingleNode("detail").SelectSingleNode("error").SelectSingleNode("status");
                                        XmlNode codeDetailErrorNode = errorNode.SelectSingleNode("detail").SelectSingleNode("error").SelectSingleNode("code");
                                        retornoXmlCCT = new RetornoXmlCCT()
                                        {
                                            message = messageDetailErrorNode.InnerText,
                                            status = statusDetailErrorNode.InnerText,
                                            code = codeDetailErrorNode.InnerText
                                        };
                                    }
                                    else
                                    {
                                        if (messageNode != null)
                                        {
                                            retornoXmlCCT = new RetornoXmlCCT()
                                            {
                                                message = messageNode.InnerText,
                                                status = statusNode.InnerText,
                                                code = codeNode.InnerText != null ? codeNode.InnerText : ""
                                            };
                                        }
                                    }
                                }
                            }

                            responseCct.Add(retornoXmlCCT);

                            List<IdentificadorNFE> _listIdent = GetIdenficadores(file);
                            responseCct[i].Identificador = _listIdent[i].Identificador;
                        }
                    }
                    catch (Exception ex)
                    {
                        responseCct.Add(new RetornoXmlCCT() { message = "Erro generico: ENTRE EM CONTATO COM ADMINISTRADOR ---- " + ex.Message });
                    }


                    GridView1.DataSource = responseCct;
                    GridView1.DataBind();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Attributes.Add("style", "cursor:help;");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[2].Text == "CCTR-ER0029")
                {
                    e.Row.BackColor = Color.FromArgb(255, 238, 186);
                }
                if (e.Row.Cells[2].Text == "CCTR-ER8899")
                {
                    e.Row.BackColor = Color.FromArgb(245, 198, 203);
                }
                if (e.Row.Cells[2].Text == "CCTR-ER8899")
                {
                    e.Row.BackColor = Color.FromArgb(245, 198, 203);
                }
                if (e.Row.Cells[2].Text == "CCTR-ER0122")
                {
                    e.Row.BackColor = Color.FromArgb(245, 198, 203);
                }
                if (e.Row.Cells[2].Text == "CCTR-1")
                {
                    e.Row.BackColor = Color.FromArgb(255, 238, 186);
                }
                if (e.Row.Cells[2].Text == "CCTR-2")
                {
                    e.Row.BackColor = Color.FromArgb(255, 238, 186);
                }
            }
        }

        public List<IdentificadorNFE> GetIdenficadores(string file)
        {
            string nomeArquivo = Server.MapPath("Uploads\\");
            string[] source = File.ReadAllLines(nomeArquivo + file);
            //pego a primeira linha do array e monto em uma string
            var listaIdentificador = new List<IdentificadorNFE>();
            for (int i = 0; i < source.Count(); i++)
            {
                string xmlAtual = source[i];
                string[] split = xmlAtual.Split(';');
                var identificador = new IdentificadorNFE(split[0]);
                listaIdentificador.Add(identificador);
            }
            return listaIdentificador;
        }
        private List<string> ConvertXmlFileAndSendNF(string file)
        {
            List<string> _xmls = new List<string>();
            string cpfCertificado = ConfigurationManager.AppSettings["CpfCertificado"].ToString();
            // Dados do arquivo TXT
            string txtFilePath = Server.MapPath("Uploads\\");
            string[] txtLines = File.ReadAllLines(txtFilePath + file);

            try
            {
                for (int i = 0; i < txtLines.Count(); i++)
                {
                    #region DATA
                    string[] txtData = txtLines[i].Split(';');

                    // Definir o namespace e o local do arquivo XSD
                    string targetNamespace = "http://www.pucomex.serpro.gov.br/cct";
                    // Criar o documento XML
                    XmlDocument doc = new XmlDocument();
                    XmlElement root = doc.CreateElement("recepcoesNFE");
                    root.SetAttribute("xmlns", targetNamespace);
                    root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");

                    doc.AppendChild(root);

                    // Adicionar o atributo 'xsi:schemaLocation' ao elemento raiz
                    XmlAttribute schemaLocation = doc.CreateAttribute("xsi", "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance");
                    schemaLocation.Value = "http://www.pucomex.serpro.gov.br/cct RecepcaoNFE.xsd";
                    root.SetAttributeNode(schemaLocation);

                    // Adicionar o elemento 'recepcaoNFE'
                    XmlElement recepcaoNFE = doc.CreateElement("recepcaoNFE");
                    root.AppendChild(recepcaoNFE);

                    // Adicionar os elementos dentro de 'recepcaoNFE' com os dados do arquivo TXT
                    // Exemplo: Adicionar o elemento 'identificacaoRecepcao'
                    XmlElement identificacaoRecepcao = doc.CreateElement("identificacaoRecepcao");
                    identificacaoRecepcao.InnerText = txtData[0];
                    recepcaoNFE.AppendChild(identificacaoRecepcao);

                    XmlElement cnpjResp = doc.CreateElement("cnpjResp");
                    cnpjResp.InnerText = txtData[1];
                    recepcaoNFE.AppendChild(cnpjResp);

                    XmlElement local = doc.CreateElement("local");
                    recepcaoNFE.AppendChild(local);

                    XmlElement codigoURF = doc.CreateElement("codigoURF");
                    codigoURF.InnerText = txtData[2];
                    local.AppendChild(codigoURF);

                    XmlElement codigoRA = doc.CreateElement("codigoRA");
                    codigoRA.InnerText = txtData[3];
                    local.AppendChild(codigoRA);

                    XmlElement notasFiscais = doc.CreateElement("notasFiscais");
                    recepcaoNFE.AppendChild(notasFiscais);

                    XmlElement notaFiscalEletronica = doc.CreateElement("notaFiscalEletronica");
                    notasFiscais.AppendChild(notaFiscalEletronica);

                    string chaveValue = txtData[4];
                    if (chaveValue.Length < 44)
                    {
                        chaveValue = chaveValue.PadLeft(44, '0');
                    }
                    XmlElement chaveAcesso = doc.CreateElement("chaveAcesso");
                    chaveAcesso.InnerText = chaveValue;
                    notaFiscalEletronica.AppendChild(chaveAcesso);

                    XmlElement transportador = doc.CreateElement("transportador");
                    recepcaoNFE.AppendChild(transportador);

                    if (!string.IsNullOrEmpty(txtData[5]))
                    {
                        string cnpjValue = txtData[5];
                        if (cnpjValue.Length > 11)
                        {
                            XmlElement cnpj = doc.CreateElement("cnpj");
                            cnpj.InnerText = cnpjValue;
                            transportador.AppendChild(cnpj);
                        }
                        else
                        {
                            XmlElement cpf = doc.CreateElement("cpf");
                            cpf.InnerText = cnpjValue;
                            transportador.AppendChild(cpf);
                        }
                    }

                    if (!string.IsNullOrEmpty(txtData[6]))
                    {
                        string cpfValue = txtData[6];
                        if (cpfValue.Length < 11)
                        {
                            cpfValue = cpfValue.PadLeft(11, '0');
                        }
                        XmlElement cpfCondutor = doc.CreateElement("cpfCondutor");
                        cpfCondutor.InnerText = cpfValue;
                        transportador.AppendChild(cpfCondutor);
                    }

                    XmlElement pesoAferido = doc.CreateElement("pesoAferido");
                    pesoAferido.InnerText = txtData[7];
                    recepcaoNFE.AppendChild(pesoAferido);

                    XmlElement observacoesGerais = doc.CreateElement("observacoesGerais");
                    observacoesGerais.InnerText = txtData[8];
                    recepcaoNFE.AppendChild(observacoesGerais);

                    string xmlString = doc.OuterXml;
                    //Enviar nota 
                    _xmls.Add(SendNfeCct(cpfCertificado, xmlString));
                    #endregion
                }
            }
            catch (Exception ex)
            {
                _xmls.Add("Erro Generico: " + ex.Message);
                return _xmls;
            }


            return _xmls;
        }

        private string SendNfeCct(string cpf, string xml)
        {
            string url = "/cct/api/ext/carga/recepcao-nfe";
            //criar o HEADER da solicitacao
            var headers = SisComexService.ObterHeaders(this.GenerateToken(cpf));
            //
            var response = SisComexService.CriarRequestNew(url, headers, xml);
            //
            return response.Content.ReadAsStringAsync().Result;

        }

        private Token GenerateToken(string cpfCertificado)
        {
            Token token = new Token();
            if (HttpContext.Current.Session["TOKEN"] == null)
            {
                token = SisComexService.ObterToken(cpfCertificado);
            }
            else
            {
                token = (Token)HttpContext.Current.Session["TOKEN"];
            }

            return token;
        }

    }
}