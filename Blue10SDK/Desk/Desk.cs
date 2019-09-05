using Blue10SDK.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace Blue10SDK.Desk
{
    public class Desk
    {
        IHttpClientFactory mHttpClientFactory;
        private string mApiKey { get; set; }
        private string mApiUrl { get; set; }

        public Desk(string apiKey, IHttpClientFactory httpClientFactory)
        {
            mHttpClientFactory = httpClientFactory;
            mApiUrl = "https://api.blue10.com/v2";
            mApiKey = apiKey;
            //Todo example
            using (var client = httpClientFactory.CreateClient())
            {
                //DOE DINGEN MET HTTP CLIENT
            }

        }

        public Desk(string apiKey, string apiUrl, IHttpClientFactory httpClientFactory)
        {
            mHttpClientFactory = httpClientFactory;
            mApiUrl = apiUrl;
            mApiKey = apiKey;
        }

        // alles documenteren
        public List<Company> GetCompanies()
        {
            var fUrl = $"{mApiUrl}/companies";
            using (var client = mHttpClientFactory.CreateClient())
            {
                //DOE DINGEN MET HTTP CLIENT
                //client.BaseAddress = new Uri(fUrl);
                //var fResult = client.Get .GetStringAsync(fUrl).Wait();
            }

            var fResult = "Json Response";
            
            return null;
        }
    }
}
