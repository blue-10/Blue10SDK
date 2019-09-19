using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Blue10SDK;

namespace Tests
{
    public class DummyWebApiClient : IWebApiAdapter
    {
        
        public Dictionary<string,object> Stash = new Dictionary<string, object>();
        
        
        public Task<TResult> GetAsync<TResult>(string pUrl)
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