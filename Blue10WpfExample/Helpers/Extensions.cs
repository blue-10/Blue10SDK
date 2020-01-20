using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blue10SdkWpfExample
{
    static class Extensions
    {
        public static T Clone<T>(this T pToClone)
        {
            var fJson = JsonConvert.SerializeObject(pToClone);
            var fRet = (T)JsonConvert.DeserializeObject(fJson, typeof(T));
            return fRet;
        }
    }
}
