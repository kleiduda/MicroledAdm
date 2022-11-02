using Sistema.DUE.Web.Config;
using Sistema.DUE.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Caching;
using Cargill.DUE.Web.Models;
using System.Linq;
using System;

namespace Sistema.DUE.Web.DAO
{
    public class TotalConsultaSerproDAO
    {
        public TotalDeConsultasRealizadas ObterTotalDeConsultas()
        {
            //MemoryCache cache = MemoryCache.Default;

            //var consultas = cache["TotalDeConsultasRealizadas.ObterTotalDeConsultas"] as TotalDeConsultasRealizadas;

            var consultas = new TotalDeConsultasRealizadas();
            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                consultas = con.Query<TotalDeConsultasRealizadas>(@"SELECT [total] as ConsultasRealizadas  FROM [adm_due].[dbo].[Total_consulta_NFe]").FirstOrDefault();
            }

            return consultas;
        }

        public void GravarConsultaRealizad()
        {
            int updateConsulta = 0;
            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                //var consultas = con.Query<TotalDeConsultasRealizadas>(@"select a.ID_Usuario as IdUsuario, a.Data_Consulta as DataConsulta, a.Qtde as QtdeDiaria
                //                                                     from TB_LOG_CONSULTA_NFe a
                //                                                     where a.ID_Usuario = 1
                //                                                     order by Data_consulta desc").FirstOrDefault();
                //if (consultas.DataConsulta.Day < DateTime.Now.Day)
                //{
                //    updateConsulta = con.Execute("INSERT INTO TB_LOG_CONSULTA_NFe (ID_Usuario, Data_Consulta, Qtde) VALUES (1, GETDATE(), 1)");
                //}
                //else
                //{
                //    updateConsulta = con.Execute(@"UPDATE TB_LOG_CONSULTA_NFe SET qtde = @qtde WHERE ID_Usuario = 1 AND Day(Data_consulta) = DAY(GETDATE())", new { qtde = consultas.QtdeDiaria + 1 });
                //}
                updateConsulta = con.Execute("INSERT INTO TB_LOG_CONSULTA_NFe (ID_Usuario, Data_Consulta, Qtde) VALUES (1, GETDATE(), 1)");
            }

        }
    }
}