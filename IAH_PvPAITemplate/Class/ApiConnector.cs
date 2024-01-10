using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace IAH_PvPAITemplate.Class
{
    public class ApiConnector
    {
        HttpClient httpClient;

        public ApiConnector(string uri)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient GetHttpClient()
        {
            return httpClient;
        }
    }
}