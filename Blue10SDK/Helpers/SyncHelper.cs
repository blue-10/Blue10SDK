using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blue10SDK
{
    public static class SyncHelper
    {
        private static readonly TaskFactory mTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunAsyncAsSync<TResult>(Func<Task<TResult>> pFunc)
        {
            var fCultureUi = CultureInfo.CurrentUICulture;
            var fCulture = CultureInfo.CurrentCulture;
            return mTaskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = fCulture;
                Thread.CurrentThread.CurrentUICulture = fCultureUi;
                return pFunc();
            }).Unwrap().GetAwaiter().GetResult();
        }
    }
}
