using Blue10SDK.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public class Desk
    {
        IHttpClientFactory mHttpClientFactory;

        public Desk(IHttpClientFactory httpClientFactory)
        {
            mHttpClientFactory = httpClientFactory;

            JsonConvert.DefaultSettings = (() =>
            {
                var fSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Auto, // for Array of basetype to deserialize polymorphic types..
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    NullValueHandling = NullValueHandling.Ignore
                };
                return fSettings;
            });

        }

        // alles documenteren
        public List<Company> GetCompanies()
        {          
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/companies";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(()=> GetCompanies(client, fUrl));
                return fRet;
            }
        }

        private async Task<List<Company>> GetCompanies(HttpClient client, string url)
        {
            var fRet = await Blue10ApiHelper.GetAsync<List<Company>>(client, url);
            return fRet;
        }

        public List<Vendor> GetVendors(string pCompanyCode)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors/{pCompanyCode}";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetVendors(client, fUrl));
                return fRet;
            }
        }

        private async Task<List<Vendor>> GetVendors(HttpClient client, string url)
        {
            var fRet = await Blue10ApiHelper.GetAsync<List<Vendor>>(client, url);
            return fRet;
        }

        public Vendor AddVendor(Vendor pVendor)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddVendor(client, pVendor, fUrl));
                return fRet;
            }
        }

        private async Task<Vendor> AddVendor(HttpClient client, Vendor pVendor, string url)
        {
            var fRet = await Blue10ApiHelper.PostAsync<Vendor>(client, pVendor, url);
            return fRet;
        }
    }
}
