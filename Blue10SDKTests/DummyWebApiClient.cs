using System.Threading.Tasks;
using Blue10SDK;

namespace Tests
{
    public class DummyWebApiClient : IWebApiAdapter
    {
        public Task<TResult> GetAsync<TResult>(string pUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<TObject> PostAsync<TObject>(TObject pObject, string pUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<TObject> PutAndReturnAsync<TObject>(TObject pObject, string pUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> PutAsync<TObject>(TObject pObject, string pUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(string pUrl)
        {
            throw new System.NotImplementedException();
        }
    }
}