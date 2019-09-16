using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class httpClientHelper : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            var fApiKey = GetValue("ApiKey");
            var fApiUrl = GetValue("ApiUrl");
            if (string.IsNullOrEmpty(fApiUrl)) fApiUrl = "https://api.blue10.com/v2";
            var fHttpClient = new HttpClient()
            {
                BaseAddress = new Uri(fApiUrl),
                Timeout = TimeSpan.FromMinutes(3)
            };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            fHttpClient.DefaultRequestHeaders.Add("Authorization", $"access_token {fApiKey}");
            fHttpClient.DefaultRequestHeaders.Add("content-typen", "application/json");
            return fHttpClient;
        }

        private string GetValue(string pKey)
        {
            var fRes = ConfigurationManager.AppSettings[pKey];
            return fRes;
        }
    }
}
