using System;
using System.Configuration;

namespace Sistema.DUE.Web
{
    public partial class ConsultarDadosDUE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["due"] != null)
            {
                using (var ws = new Cargill.DUE.Web.ServicoSiscomex.WsDUE())
                {
                    try
                    {
                        var dadosCompletos = new Cargill.DUE.Web.ServicoSiscomex.DueDadosCompletos();

                        dadosCompletos = ws.ObterDadosCompletos(Request.QueryString["due"].ToString(), ConfigurationManager.AppSettings["CpfCertificado"].ToString());
                        
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }                      
        }
    }
}