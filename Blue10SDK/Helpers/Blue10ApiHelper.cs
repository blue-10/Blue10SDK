using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public static class Blue10ApiHelper
    {
        private static T DeserializeTo<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
     

        public static async Task<TResult> GetAsync<TResult>(HttpClient pHttpCLient, string pUrl)
        {
            try
            {
                var fResponseHttp = await pHttpCLient.GetAsync(pUrl);
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

        public static async Task<TObject> PostAsync<TObject>(HttpClient pHttpCLient, TObject pObject, string pUrl)
        {
            try
            {
                var fJsonObject = JsonConvert.SerializeObject(pObject);
                var fHttpContent = new StringContent(fJsonObject);
                var fResponseHttp = await pHttpCLient.PostAsync(pUrl, fHttpContent);
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

        public static async Task<TObject> PutAndReturnAsync<TObject>(HttpClient pHttpCLient, TObject pObject, string pUrl)
        {
            try
            {
                var fJsonObject = JsonConvert.SerializeObject(pObject);
                var fHttpContent = new StringContent(fJsonObject);
                var fResponseHttp = await pHttpCLient.PutAsync(pUrl, fHttpContent);
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

        public static async Task<string> PutAsync<TObject>(HttpClient pHttpCLient, TObject pObject, string pUrl)
        {
            try
            {
                var fJsonObject = JsonConvert.SerializeObject(pObject);
                var fHttpContent = new StringContent(fJsonObject);
                var fResponseHttp = await pHttpCLient.PutAsync(pUrl, fHttpContent);
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

        public static async Task<bool> DeleteAsync(HttpClient pHttpCLient, string pUrl)
        {
            try
            {
                var fResponseHttp = await pHttpCLient.DeleteAsync(pUrl);
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
}
