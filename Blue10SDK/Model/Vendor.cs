using System.Collections.Generic;

namespace Blue10SDK
{
    public class Vendor : BaseObject
    {
        public string name { get; set; }
        public string vat_number { get; set; }
        public string country_code { get; set; } = "NL";
        public List<string> iban { get; set; }
        public string currency_code { get; set; } = "EUR";
        public string vendor_customer_code { get; set; }
        public string default_ledger_code { get; set; } = string.Empty;
        public string default_vat_code { get; set; } = string.Empty;
        public string default_vat_scenario_code { get; set; } = string.Empty;
        public string default_payment_term_code { get; set; } = string.Empty;
        public bool blocked { get; set; }
    }
}
