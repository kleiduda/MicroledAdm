using Auth0.AuthenticationApi.Models;
using Cargill.DUE.Web.Models;
using Cargill.DUE.Web.Models.SERPRO.xml;
using Cargill.DUE.Web.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sistema.DUE.Web.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Cargill.DUE.Web.Services
{
    public class ConsultaSerpro
    {
        string key = "meGUkBLsHfeDycdu89IYwTgdnzIa";
        string secret = "qz1JoOAlfbjCugGpGfEZOBqRNU8a";
        string baseUrl = "https://gateway.apiserpro.serpro.gov.br/token";

        private readonly TotalConsultaSerproDAO _consultaSerproDao = new TotalConsultaSerproDAO();

        public ConsultaAverbacoesNFE GetConsultaChaveNfe(string chaveNfe)
        {
            throw new NotImplementedException();
        }

        public ConsultaAverbacoesNFE ConsultaAverbacoesNFE(string accessToken, string chave)
        {
            ConsultaAverbacoesNFE cons = new ConsultaAverbacoesNFE();
            cons = GetNfe(accessToken, chave).Result;

            return cons;
        }

        public async Task<ConsultaAverbacoesNFE> GetNfe(string accessToken, string chave)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ConsultaAverbacoesNFE consultaAverbacoes = null;
            try
            {
                string getNfeEndpoint = "https://gateway.apiserpro.serpro.gov.br/consulta-nfe-df/api/v1/nfe/" + chave;

                HttpClient client = Method_Headers(accessToken, getNfeEndpoint);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri.EscapeUriString(client.BaseAddress.ToString()));
                HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                string responseBody = await tokenResponse.Content.ReadAsStringAsync();
                string newJson = responseBody.Replace("\"itensAverbados\": {", "\"itensAverbados\": {[");


                if (tokenResponse.IsSuccessStatusCode)
                {
                    object jsonObj = JsonConvert.DeserializeObject<object>(responseBody);
                    //consultaAverbacoes = JsonConvert.DeserializeObject<ConsultaAverbacoesNFE>(responseBody);
                    JObject obj = JObject.Parse(responseBody);
                    //
                    //

                    JArray jarr = (JArray)obj["procEventoNFe"];

                    foreach (var item in jarr.Children())
                    {
                        var itemProperty = item.Children<JProperty>().Children().Children().Children().Children().Children();
                    }
                    _consultaSerproDao.GravarConsultaRealizad();
                    consultaAverbacoes = tokenResponse.Content.ReadAsAsync<ConsultaAverbacoesNFE>(new[] { new JsonMediaTypeFormatter() }).Result;
                }
                else
                {
                    if (((int)tokenResponse.StatusCode) == 400)
                    {
                        consultaAverbacoes = new ConsultaAverbacoesNFE()
                        {
                            Error = "NFE não localizada ou invalida"
                        };
                    }
                }
            }
            catch (HttpRequestException ex)
            {

            }
            return consultaAverbacoes;
        }
        public async Task<string> GetXml(string accessToken, string chave)
        {
            string xmlResult = null;
            try
            {
                string getNfeEndpoint = "https://gateway.apiserpro.serpro.gov.br/consulta-nfe-df/api/v1/nfe/" + chave;

                HttpClient client = Method_HeadersXml(accessToken, getNfeEndpoint);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Uri.EscapeUriString(client.BaseAddress.ToString()));
                HttpResponseMessage tokenResponse = await client.GetAsync(Uri.EscapeUriString(client.BaseAddress.ToString()), HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                if (tokenResponse.IsSuccessStatusCode)
                {
                    _consultaSerproDao.GravarConsultaRealizad();
                    xmlResult = tokenResponse.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    if (((int)tokenResponse.StatusCode) == 400)
                    {
                        xmlResult = "NFE não localizada ou invalida";
                    }
                }
            }
            catch (HttpRequestException ex)
            {

            }
            return xmlResult;
        }


        private HttpClient Method_Headers(string accessToken, string endpointURL)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            HttpClient client = new HttpClient(handler);

            try
            {
                client.BaseAddress = new Uri(endpointURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("apikey", apikey);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return client;
        }

        private HttpClient Method_HeadersXml(string accessToken, string endpointURL)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            HttpClient client = new HttpClient(handler);

            try
            {
                client.BaseAddress = new Uri(endpointURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                //client.DefaultRequestHeaders.Add("apikey", apikey);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return client;
        }

        public async Task<string> GenerateAccessToken()
        {
            AccessTokenResponse token = null;

            try
            {
                HttpClient client = HeadersForAccessTokenGenerate();
                string body = "grant_type=client_credentials";
                client.BaseAddress = new Uri(baseUrl);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);
                request.Content = new StringContent(body,
                                                    Encoding.UTF8,
                                                    "application/x-www-form-urlencoded");//CONTENT-TYPE header  

                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

                postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                //postData.Add(new KeyValuePair<string, string>("scope", "public"));

                request.Content = new FormUrlEncodedContent(postData);
                HttpResponseMessage tokenResponse = client.PostAsync(baseUrl, new FormUrlEncodedContent(postData)).Result;

                //var token = tokenResponse.Content.ReadAsStringAsync().Result;    
                token = await tokenResponse.Content.ReadAsAsync<AccessTokenResponse>(new[] { new JsonMediaTypeFormatter() });
            }


            catch (HttpRequestException ex)
            {
                throw ex;
            }
            return token != null ? token.AccessToken : null;

        }
        private HttpClient HeadersForAccessTokenGenerate()
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
            HttpClient client = new HttpClient(handler);
            try
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("apikey", apikey);
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(
                         System.Text.ASCIIEncoding.ASCII.GetBytes($"{key}:{secret}")));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return client;
        }

        public TotalDeConsultasRealizadas TotalConsultasREalizadas()
        {
            TotalDeConsultasRealizadas totalDeConsultasRealizadas = new TotalDeConsultasRealizadas();


            return totalDeConsultasRealizadas;
        }
    }
}