using Sistema.DUE.Web.Config;
using Sistema.DUE.Web.Models;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Caching;
using Cargill.DUE.Web.Models;
using System.Linq;
using System;
using Cargill.DUE.Web.Models.SERPRO;
using System.Data;
using System.Web.Optimization;
using System.Transactions;

namespace Sistema.DUE.Web.DAO
{
    public class TotalConsultaSerproDAO
    {
        public int ObterTotalDeConsultas()
        {
            //MemoryCache cache = MemoryCache.Default;

            //var consultas = cache["TotalDeConsultasRealizadas.ObterTotalDeConsultas"] as TotalDeConsultasRealizadas;

            int totalconsultas = 0;
            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                totalconsultas = con.Query<int>(@"SELECT * FROM [adm_due].[dbo].[Total_consulta_NFe]").FirstOrDefault();
            }

            return totalconsultas;
        }

        public void GravarConsultaRealizad(int quantidade)
        {
            string query = @"INSERT INTO TB_LOG_CONSULTA_NFe (ID_Usuario, Data_Consulta, Qtde) VALUES (1, GETDATE(), @Quantidade)";
            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                con.Execute(query, new {Quantidade = quantidade});
            }
        }

        public void GravarConsultaNaBase(List<ConsultaSerproView> consultaSerproViews)
        {
            string _delete = @"IF EXISTS (SELECT 1 FROM TB_NF_Pesquisada_SEFAZ WHERE ChaveNfe = @chaveNfe)
                               BEGIN
                                   DELETE FROM TB_NF_Pesquisada_SEFAZ WHERE ChaveNfe = @chaveNfe;
                               END";
            string _insert = @"INSERT INTO TB_NF_Pesquisada_SEFAZ 
                               (ChaveNfe
                               ,ItemNfe
                               ,UnidadeTributavel
                               ,QtdeTributavel
                               ,DataDoEmbarque
                               ,DataAverbacao
                               ,QtdeAverbada
                               ,Due
                               ,ItemDue
                               ,Descricao
                               ,ArquivoXML
                               ,DataDaConsulta)
                               VALUES (@chaveNfe, @itemNfe, @unidadeTributavel, @qtdeTributavel, @dataDoEmbarque, @dataAverbacao, @qtdeAverbada, @due, @itemDue, @descricao, @arquivoXml, GETDATE())";
            string _log = @"";

            using (var connection = new SqlConnection(Banco.StringConexao()))
            {
                connection.Open();
                connection.Execute(_delete, new { chaveNfe = consultaSerproViews[0].ChaveNfe });
            }

            DynamicParameters param = new DynamicParameters();
            for (int i = 0; i < consultaSerproViews.Count; i++)
            {
                param.Add("chaveNfe", consultaSerproViews[i].ChaveNfe, DbType.String, ParameterDirection.Input);
                param.Add("itemNfe", consultaSerproViews[i].ItemNfe != null ? consultaSerproViews[i].ItemNfe : null);
                param.Add("unidadeTributavel", consultaSerproViews[i].UnidadeTributavel, DbType.String, ParameterDirection.Input);
                param.Add("qtdeTributavel", consultaSerproViews[i].QtdeTributavel, DbType.String, ParameterDirection.Input);
                param.Add("dataDoEmbarque", consultaSerproViews[i].DataDoEmbarque != null ? consultaSerproViews[i].DataDoEmbarque : null, DbType.DateTime, ParameterDirection.Input);
                param.Add("dataAverbacao", consultaSerproViews[i].DataAverbacao != null ? consultaSerproViews[i].DataAverbacao : null, DbType.DateTime, ParameterDirection.Input);
                param.Add("qtdeAverbada", consultaSerproViews[i].QtdeAverbada != null ? consultaSerproViews[i].QtdeAverbada : null, DbType.String, ParameterDirection.Input);
                param.Add("due", consultaSerproViews[i].Due != null ? consultaSerproViews[i].Due : null, DbType.String, ParameterDirection.Input);
                param.Add("itemDue", consultaSerproViews[i].ItemDue != null ? consultaSerproViews[i].ItemDue : null, DbType.String, ParameterDirection.Input);
                param.Add("descricao", consultaSerproViews[i].Descricao, DbType.String, ParameterDirection.Input);
                param.Add("arquivoXml", consultaSerproViews[i].ArquivoXml, DbType.String, ParameterDirection.Input);
                //
                using (var connection = new SqlConnection(Banco.StringConexao()))
                {
                    connection.Open();
                    connection.Execute(_insert, param);
                }
            }
        }

        public List<ConsultaSerproView> BuscarConsultasNaBase(List<string> chaves)
        {
            List<ConsultaSerproView> consultas = new List<ConsultaSerproView>();

            string query = @"SELECT * 
                             FROM TB_NF_Pesquisada_SEFAZ a
                             WHERE a.ChaveNfe in @chaves";

            DynamicParameters param = new DynamicParameters();
            param.Add("chaves", chaves);

            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                var result = con.Query<ConsultaSerproView>(query, param).ToList();
                consultas = result;
            }

            return consultas;
        }
        public ConsultaSerproView ConsultaPriorizadaNaBase(string chave)
        {
            ConsultaSerproView consultas = new ConsultaSerproView();

            string query = @"SELECT * 
                             FROM TB_NF_Pesquisada_SEFAZ a
                             WHERE a.ChaveNfe = @chave";

            DynamicParameters param = new DynamicParameters();
            param.Add("chave", chave);

            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                var result = con.Query<ConsultaSerproView>(query, param).FirstOrDefault();
                consultas = result;
            }

            return consultas;
        }
        public List<string> DownloadXmlFile(List<string> chaves)
        {

            List<string> xmls = new List<string>();
            List<string> xmlsResult = new List<string>();
            string query = @"SELECT a.ArquivoXml 
                             FROM TB_NF_Pesquisada_SEFAZ a
                             WHERE a.ChaveNfe in @chaves";

            DynamicParameters param = new DynamicParameters();
            param.Add("chaves", chaves);

            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                var result = con.Query<string>(query, param).ToList();
                xmls = result;
            }
            xmlsResult = xmls.Distinct().ToList();

            return xmlsResult;

        }
    }
}