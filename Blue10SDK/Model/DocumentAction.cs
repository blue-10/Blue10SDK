using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDK
{
    public class DocumentAction
    {
        public Guid id { get; set; }
        public string action { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public string message { get; set; }
        public DateTime creation_time { get; set; }
        public PurchaseInvoice purchase_invoice { get; set; }
    }
}
