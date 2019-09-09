using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDK
{
    public class Vendor
    {
        public string id { get; set; }
        public string id_company { get; set; }
        public string administration_code { get; set; }
        public string name { get; set; }
        public string vat_number { get; set; }
        public string country_code { get; set; } = "NL";
        public List<string> iban { get; set; }
        public string currency_code { get; set; } = "EUR";
        public string vendor_customer_code { get; set; }
        public bool blocked { get; set; }
    }
}
