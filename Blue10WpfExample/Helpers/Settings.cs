using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Blue10SdkWpfExample
{
    public class Settings
    {

        public static string ApiUrl
        {
            get
            {
                var fAppUrl = GetValue("ApiUrl");
                return (string.IsNullOrEmpty(fAppUrl)) ? "https://api.blue10.com/v2" : fAppUrl; 
            }
        }

        public static string ApiKey
        {
            get
            {
                var fApiKey = GetValue("ApiKey");
                return (string.IsNullOrEmpty(fApiKey)) ? "" : fApiKey;
            }
        }

        private static string GetValue(string pKey)
        {
            var fRes = ConfigurationManager.AppSettings[pKey];
            return fRes;
        }
    }
}
