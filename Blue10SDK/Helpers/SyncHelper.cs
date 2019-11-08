using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public static class SyncHelper
    {
        private static TResult RunAsyncAsSync<TResult>(Func<Task<TResult>> pFunc)
        {
            var fCultureUi = CultureInfo.CurrentUICulture;
            var fCulture = CultureInfo.CurrentCulture;
            return Task.Run(() =>
            {
                Thread.CurrentThread.CurrentCulture = fCulture;
                Thread.CurrentThread.CurrentUICulture = fCultureUi;
                return pFunc();
            }).GetAwaiter().GetResult();
        }

        public static TResult Sync<TResult>(this Task<TResult> pTask) => RunAsyncAsSync(()=>pTask);
    }
}