using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blue10SDK.Exceptions;
using Blue10SDK.Json;

namespace Blue10SDK
{
    public class B10WebApiAdapter : IWebApiAdapter
    {
        #region Fields
        
        private IHttpClientFactory mHttpClientFactory;

        #endregion

        #region Constructors

        public B10WebApiAdapter(IHttpClientFactory pHttpClientFactory)
        {
            mHttpClientFactory = pHttpClientFactory;
        }

        #endregion

        public async Task<TResult> GetAsync<TResult>(string pUrl) 
        {
            using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
            var fResponseHttp = await pHttpClient.GetAsync(pUrl);
            var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            var fResponsObject = JsonSerializer.Deserialize<JsonDataResult<TResult>>(fJson, DefaultJsonSerializerOptions.Options);
            if (fResponsObject == null) return default;
            if (fResponsObject.code == 200) return fResponsObject.data;
            throw new Blue10ApiException(fResponsObject.message);
        }

        public async Task<List<TResult>> GetAsyncList<TResult>(string pUrl) 
        {
            using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
            var fResponseHttp = await pHttpClient.GetAsync(pUrl);
            var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            var fResponsObject = JsonSerializer.Deserialize<JsonDataResult<List<TResult>>>(fJson, DefaultJsonSerializerOptions.Options);
            if (fResponsObject == null) return default;
            if (fResponsObject.code != 200) throw new Blue10ApiException(fResponsObject.message);
            List<TResult> fRet = fResponsObject.data;
            while (!string.IsNullOrWhiteSpace(fResponsObject.next))
            {
                fResponseHttp = await pHttpClient.GetAsync(fResponsObject.next);
                fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
                fResponsObject = JsonSerializer.Deserialize<JsonDataResult<List<TResult>>>(fJson, DefaultJsonSerializerOptions.Options);
                if (fResponsObject == null) return default;
                if (fResponsObject.code != 200) throw new Blue10ApiException(fResponsObject.message);
                fRet.AddRange(fResponsObject.data);
            }
            return fRet;

        }

        public async Task<TObject> PostAsync<TObject>(TObject pObject, string pUrl)
        {
            using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
            var fJsonObject = JsonSerializer.Serialize(pObject, DefaultJsonSerializerOptions.Options);
            var fHttpContent = new StringContent(fJsonObject);
            var fResponseHttp = await pHttpClient.PostAsync(pUrl, fHttpContent);
            var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            var fResponsObject = JsonSerializer.Deserialize<JsonDataResult<TObject>>(fJson, DefaultJsonSerializerOptions.Options);
            if (fResponsObject == null) return default;
            if (fResponsObject.code == 201) return fResponsObject.data;
            throw new Blue10ApiException(fResponsObject.message);
        }

        public async Task<TObject> PutAndReturnAsync<TObject>(TObject pObject, string pUrl)
        {
            using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
            var fJsonObject = JsonSerializer.Serialize(pObject, DefaultJsonSerializerOptions.Options);
            var fHttpContent = new StringContent(fJsonObject);
            var fResponseHttp = await pHttpClient.PutAsync(pUrl, fHttpContent);
            var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            var fResponsObject = JsonSerializer.Deserialize<JsonDataResult<TObject>>(fJson, DefaultJsonSerializerOptions.Options);
            if (fResponsObject == null) return default;
            if (fResponsObject.code == 202) return fResponsObject.data;
            throw new Blue10ApiException(fResponsObject.message);
        }

        public async Task<string> PutAsync<TObject>(TObject pObject, string pUrl)
        {
            using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
            var fJsonObject = JsonSerializer.Serialize(pObject, DefaultJsonSerializerOptions.Options);
            var fHttpContent = new StringContent(fJsonObject);
            var fResponseHttp = await pHttpClient.PutAsync(pUrl, fHttpContent);
            var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            var fResponsObject = JsonSerializer.Deserialize<JsonDataResult<string>>(fJson, DefaultJsonSerializerOptions.Options);
            if (fResponsObject == null) return default;
            if (fResponsObject.code == 200) return fResponsObject.data;
            throw new Blue10ApiException(fResponsObject.message);
        }

        public async Task<bool> DeleteAsync(string pUrl)
        {
            using var pHttpClient = mHttpClientFactory.CreateClient(nameof(B10WebApiAdapter));
            var fResponseHttp = await pHttpClient.DeleteAsync(pUrl);
            var fJson = await fResponseHttp.Content.ReadAsStringAsync().ConfigureAwait(false);
            var fResponsObject = JsonSerializer.Deserialize<JsonDataResult<string>>(fJson, DefaultJsonSerializerOptions.Options);
            if (fResponsObject == null) return default;
            if (fResponsObject.code == 200 && fResponsObject.status == "success") return true;
            throw new Blue10ApiException(fResponsObject.message);
        }
    }
}