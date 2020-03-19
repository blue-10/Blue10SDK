using System.Collections.Generic;
using System.Threading.Tasks;
using Blue10SDK;

namespace Blue10SDKTests.Stubs
{
    public class WebApiAdapterStub : IWebApiAdapter
    {
        public Dictionary<string,object> Stash = new Dictionary<string, object>();
        
        public Task<TResult> GetAsync<TResult>(string pUrl)
        {
            //Todo get shour return something from the stash
            return null;
        }

        public Task<List<TResult>> GetAsyncList<TResult>(string pUrl)
        {
            //Todo get shour return something from the stash
            return null;
        }

        public Task<TObject> PostAsync<TObject>(TObject pObject, string pUrl)
        {
            Stash[pUrl] = pObject;
            return Task.FromResult(pObject);
        }

        public Task<TObject> PutAndReturnAsync<TObject>(TObject pObject, string pUrl)
        {
            Stash[pUrl] = pObject;
            return Task.FromResult(pObject);
        }

        public Task<string> PutAsync<TObject>(TObject pObject, string pUrl)
        {
            Stash[pUrl] = pObject;
            //Todo
            return Task.FromResult("Something");
        }

        public Task<bool> DeleteAsync(string pUrl)
        {
            Stash.Remove(pUrl);
            return Task.FromResult(true);
        }

        
    }
}