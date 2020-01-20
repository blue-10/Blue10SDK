using Blue10SDK.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blue10SdkWpfExample
{
    public class VendorWpf : Vendor
    {
        public string Ibans
        {
            get
            {
                if (Iban == null) return string.Empty;
                return string.Join(',', Iban);
            }
            set
            {
                if (string.IsNullOrEmpty(value)) Iban = null;
                else
                {
                    Iban = value.Split(',').Select(i => i.Trim()).ToList();
                }
            }
        }

        public static List<VendorWpf> GetFromVendors(List<Vendor> pList)
        {
            var fJson = JsonConvert.SerializeObject(pList);
            var fRet = (List<VendorWpf>)JsonConvert.DeserializeObject(fJson, typeof(List<VendorWpf>));
            return fRet;
        }
    }
}
