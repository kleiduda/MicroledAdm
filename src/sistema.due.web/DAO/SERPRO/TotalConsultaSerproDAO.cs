﻿using Sistema.DUE.Web.Config;
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
                //gravar o objeto total e gravar na base - TODO
                //cada vez que entrar grava a quantidade de consultas validas - TODO
                var consultas = con.Query<TotalDeConsultasRealizadas>(@"select a.ID_Usuario as IdUsuario, a.Data_Consulta as DataConsulta, a.Qtde as QtdeDiaria
                                                                     from TB_LOG_CONSULTA_NFe a
                                                                     where a.ID_Usuario = 1
                                                                     order by Data_consulta desc").FirstOrDefault();
                if (consultas.DataConsulta.Day < DateTime.Now.Day)
                {
                    updateConsulta = con.Execute("INSERT INTO TB_LOG_CONSULTA_NFe (ID_Usuario, Data_Consulta, Qtde) VALUES (1, GETDATE(), 1)");
                }
                else
                {
                    updateConsulta = con.Execute(@"UPDATE TB_LOG_CONSULTA_NFe SET qtde = @qtde WHERE ID_Usuario = 1 AND Day(Data_consulta) = DAY(GETDATE())", new { qtde = consultas.QtdeDiaria + 1 });
                }
                //updateConsulta = con.Execute("INSERT INTO TB_LOG_CONSULTA_NFe (ID_Usuario, Data_Consulta, Qtde) VALUES (1, GETDATE(), 1)");
            }

        }

        public void GravarConsultaNaBase(ConsultaSerproView consultaSerproViews)
        {
            string queryConsulta = @"SELECT 1 FROM TB_NF_Pesquisada_SEFAZ WHERE ChaveNfe = @ChaveNfe";
            string query = @"INSERT INTO TB_NF_Pesquisada_SEFAZ 
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

            string query2 = @"
                                BEGIN
                                    DELETE TB_NF_Pesquisada_SEFAZ 
                                    WHERE ChaveNfe = @chaveNfe 
                                END
                              ELSE
                                BEGIN
                                    INSERT INTO TB_NF_Pesquisada_SEFAZ 
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
                                    VALUES (@chaveNfe, @itemNfe, @unidadeTributavel, @qtdeTributavel, @dataDoEmbarque, @dataAverbacao, @qtdeAverbada, @due, @itemDue, @descricao, @arquivoXml, GETDATE())
                                END";
            
            DynamicParameters param = new DynamicParameters();

            param.Add("chaveNfe", consultaSerproViews.ChaveNfe, DbType.String, ParameterDirection.Input);
            param.Add("itemNfe", consultaSerproViews.ItemNfe != null ? consultaSerproViews.ItemNfe : null);
            param.Add("unidadeTributavel", consultaSerproViews.UnidadeTributavel, DbType.String, ParameterDirection.Input);
            param.Add("qtdeTributavel", consultaSerproViews.QtdeTributavel, DbType.String, ParameterDirection.Input);
            param.Add("dataDoEmbarque", consultaSerproViews.DataDoEmbarque != null ? consultaSerproViews.DataDoEmbarque : null, DbType.DateTime, ParameterDirection.Input);
            param.Add("dataAverbacao", consultaSerproViews.DataAverbacao != null ? consultaSerproViews.DataAverbacao : null , DbType.DateTime, ParameterDirection.Input);
            param.Add("qtdeAverbada", consultaSerproViews.QtdeAverbada != null ? consultaSerproViews.QtdeAverbada : null, DbType.String, ParameterDirection.Input);
            param.Add("due", consultaSerproViews.Due != null ? consultaSerproViews.Due : null , DbType.String, ParameterDirection.Input);
            param.Add("itemDue", consultaSerproViews.ItemDue != null ? consultaSerproViews.ItemDue : null , DbType.String, ParameterDirection.Input);
            param.Add("descricao", consultaSerproViews.Descricao, DbType.String, ParameterDirection.Input);
            param.Add("arquivoXml", consultaSerproViews.ArquivoXml, DbType.String, ParameterDirection.Input);
            using (SqlConnection con = new SqlConnection(Banco.StringConexao()))
            {
                con.Execute(query2, param);
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