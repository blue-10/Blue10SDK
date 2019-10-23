using System.Collections.Generic;

namespace Blue10SDK.Models
{
    public class Vendor : BaseObject
    {
        public string Name { get; set; }

        public string VatNumber { get; set; }

        public string CountryCode { get; set; } = "NL";

        public List<string> Iban { get; set; }

        public string CurrencyCode { get; set; } = "EUR";

        public string VendorCustomerCode { get; set; }

        public string DefaultLedgerCode { get; set; } = string.Empty;

        public string DefaultVatCode { get; set; } = string.Empty;

        public string DefaultVatScenarioCode { get; set; } = string.Empty;

        public string DefaultPaymentTermCode { get; set; } = string.Empty;

        public bool Blocked { get; set; }
    }
}
