using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public class Desk
    {
        IHttpClientFactory mHttpClientFactory;

        public Desk(IHttpClientFactory pHttpClientFactory)
        {
            mHttpClientFactory = pHttpClientFactory;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto, // for Array of basetype to deserialize polymorphic types..
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

        }

        /// <summary>
        /// TODO Document
        /// </summary>
        /// <returns></returns>
        public List<Company> GetCompanies()
        {          
            using (var fClient = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{fClient.BaseAddress}/companies";
                fClient.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(()=> GetCompanies(fClient, fUrl));
                return fRet;
            }
        }

        private async Task<List<Company>> GetCompanies(HttpClient pClient, string pUrl) =>
            await Blue10ApiHelper.GetAsync<List<Company>>(pClient, pUrl);

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

        private async Task<List<Vendor>> GetVendors(HttpClient pClient, string pUrl) =>
            await Blue10ApiHelper.GetAsync<List<Vendor>>(pClient, pUrl);

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

        private async Task<Vendor> AddVendor(HttpClient pClient, Vendor pVendor, string pUrl)
        {
            var fRet = await Blue10ApiHelper.PostAsync(pClient, pVendor, pUrl);
            return fRet;
        }

        public Vendor EditVendor(Vendor pVendor)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors/{pVendor.id}";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditVendor(client, pVendor, fUrl));
                return fRet;
            }
        }

        private async Task<Vendor> EditVendor(HttpClient pClient, Vendor pVendor, string pUrl) =>
            await Blue10ApiHelper.PutAsync<Vendor>(pClient, pVendor, pUrl);

        public bool DeleteVendor(Vendor pVendor)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors/{pVendor.id}";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteVendor(client, fUrl));
                return fRet;
            }
        }

        private async Task<bool> DeleteVendor(HttpClient pClient, string pUrl) =>
            await Blue10ApiHelper.DeleteAsync(pClient, pUrl);
    }
}
