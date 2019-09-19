using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public class B10WebApiAdapter : IWebApiAdapter
    {

        private IHttpClientFactory mHttpClientFactory;
        public B10WebApiAdapter(IHttpClientFactory pHttpClientFactory)
        {
            mHttpClientFactory = pHttpClientFactory;
        }
        
        public async Task<TResult> GetAsync<TResult>(string pUrl)
        {
            try
            {
                using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
                var fResponseHttp = await pHttpClient.GetAsync(pUrl);
                var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fResponsObject = fJson.DeserializeTo<JsonDataResult<TResult>>();
                if (fResponsObject == null) return default;
                if (fResponsObject.code == 200) return fResponsObject.data;
                throw new Blue10ApiException(fResponsObject.message);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TObject> PostAsync<TObject>(TObject pObject, string pUrl)
        {
            try
            {
                using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
                var fJsonObject = JsonConvert.SerializeObject(pObject);
                var fHttpContent = new StringContent(fJsonObject);
                var fResponseHttp = await pHttpClient.PostAsync(pUrl, fHttpContent);
                var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fResponsObject = fJson.DeserializeTo<JsonDataResult<TObject>>();
                if (fResponsObject == null) return default;
                if (fResponsObject.code == 200) return fResponsObject.data;
                throw new Blue10ApiException(fResponsObject.message);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TObject> PutAndReturnAsync<TObject>(TObject pObject, string pUrl)
        {
            try
            {
                using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
                var fJsonObject = JsonConvert.SerializeObject(pObject);
                var fHttpContent = new StringContent(fJsonObject);
                var fResponseHttp = await pHttpClient.PutAsync(pUrl, fHttpContent);
                var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fResponsObject = fJson.DeserializeTo<JsonDataResult<TObject>>();
                if (fResponsObject == null) return default;
                if (fResponsObject.code == 200) return fResponsObject.data;
                throw new Blue10ApiException(fResponsObject.message);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> PutAsync<TObject>(TObject pObject, string pUrl)
        {
            try
            {
                using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
                var fJsonObject = JsonConvert.SerializeObject(pObject);
                var fHttpContent = new StringContent(fJsonObject);
                var fResponseHttp = await pHttpClient.PutAsync(pUrl, fHttpContent);
                var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fResponsObject = fJson.DeserializeTo<JsonDataResult<string>>();
                if (fResponsObject == null) return default;
                if (fResponsObject.code == 200) return fResponsObject.data;
                throw new Blue10ApiException(fResponsObject.message);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string pUrl)
        {
            try
            {
                using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
                var fResponseHttp = await pHttpClient.DeleteAsync(pUrl);
                var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fResponsObject = fJson.DeserializeTo<JsonDataResult<bool>>();
                if (fResponsObject == null) return default;
                if (fResponsObject.code == 200 && fResponsObject.status == "success") return true;
                throw new Blue10ApiException(fResponsObject.message);
            }
            catch
            {
                throw;
            }
        }
    }

    internal static class JsonExtension
    {
        internal static T DeserializeTo<T>(this string json) => (T)JsonConvert.DeserializeObject(json, typeof(T));
    }
}
