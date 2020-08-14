using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public interface IWebApiAdapter
    {
        Task<TResult> GetAsync<TResult>(string pUrl);

        Task<List<TResult>> GetAsyncList<TResult>(string pUrl);

        Task<TObject> PostAsync<TObject>(TObject pObject, string pUrl);

        Task<TObject> PutAndReturnAsync<TObject>(TObject pObject, string pUrl);

        Task<string> PutAsync<TObject>(TObject pObject, string pUrl);

        Task<bool> DeleteAsync(string pUrl);
    }
}