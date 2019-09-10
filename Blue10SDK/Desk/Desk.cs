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
                var fRet = SyncHelper.RunAsyncAsSync(()=> GetItems<List<Company>>(fClient, fUrl));
                return fRet;
            }
        }

        #region Vendors
        public List<Vendor> GetVendors(string pCompanyCode)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors/{pCompanyCode}";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => GetItems<List<Vendor>>(client, fUrl));
                return fRet;
            }
        }

        public Vendor AddVendor(Vendor pVendor)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => AddItem<Vendor>(client, pVendor, fUrl));
                return fRet;
            }
        }      

        public Vendor EditVendor(Vendor pVendor)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors/{pVendor.id}";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => EditItem<Vendor>(client, pVendor, fUrl));
                return fRet;
            }
        }

        public bool DeleteVendor(Vendor pVendor)
        {
            using (var client = mHttpClientFactory.CreateClient())
            {
                var fUrl = $"{client.BaseAddress}/vendors/{pVendor.id}";
                client.BaseAddress = new Uri(fUrl);
                var fRet = SyncHelper.RunAsyncAsSync(() => DeleteItem(client, fUrl));
                return fRet;
            }
        }

        #endregion 

        private async Task<T> GetItems<T>(HttpClient pClient, string pUrl) =>
            await Blue10ApiHelper.GetAsync<T>(pClient, pUrl);
        private async Task<T> AddItem<T>(HttpClient pClient, T pItem, string pUrl) =>
            await Blue10ApiHelper.PostAsync<T>(pClient, pItem, pUrl);
        private async Task<T> EditItem<T>(HttpClient pClient, T pItem, string pUrl) =>
            await Blue10ApiHelper.PutAsync<T>(pClient, pItem, pUrl);
        private async Task<bool> DeleteItem(HttpClient pClient, string pUrl) =>
            await Blue10ApiHelper.DeleteAsync(pClient, pUrl);
    }
}
