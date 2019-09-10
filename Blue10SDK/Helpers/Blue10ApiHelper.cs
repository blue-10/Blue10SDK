using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public static class Blue10ApiHelper
    {
        private static T ParseJson<T>(string json)
        {
            var fRes = (T)JsonConvert.DeserializeObject(json, typeof(T));
            return fRes;
        }

        public static async Task<TResult> GetAsync<TResult>(HttpClient pHttpCLient, string pUrl)
        {
            try
            {
                var fResponseHttp = await pHttpCLient.GetAsync(pUrl);
                var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fResponsObject = ParseJson<JsonDataResult<TResult>>(fJson);
                if (fResponsObject == null) return default;
                if (fResponsObject.Code == 200) return fResponsObject.Data;
                throw new Blue10ApiException(fResponsObject.Message);
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
                var fResponsObject = ParseJson<JsonDataResult<TObject>>(fJson);
                if (fResponsObject == null) return default;
                if (fResponsObject.Code == 200) return fResponsObject.Data;
                throw new Blue10ApiException(fResponsObject.Message);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<TObject> PutAsync<TObject>(HttpClient pHttpCLient, TObject pObject, string pUrl)
        {
            try
            {
                var fJsonObject = JsonConvert.SerializeObject(pObject);
                var fHttpContent = new StringContent(fJsonObject);
                var fResponseHttp = await pHttpCLient.PutAsync(pUrl, fHttpContent);
                var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                var fResponsObject = ParseJson<JsonDataResult<TObject>>(fJson);
                if (fResponsObject == null) return default;
                if (fResponsObject.Code == 200) return fResponsObject.Data;
                throw new Blue10ApiException(fResponsObject.Message);
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
                var fResponsObject = ParseJson<JsonDataResult<bool>>(fJson);
                if (fResponsObject == null) return default;
                if (fResponsObject.Code == 200 && fResponsObject.Status == "success") return true;
                throw new Blue10ApiException(fResponsObject.Message);
            }
            catch
            {
                throw;
            }
        }
    }
}
