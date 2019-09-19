using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blue10SDK
{
    public class DocumentAction
    {
        public Guid id { get; set; }
        
        public EDocumentAction action { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public string message { get; set; }
        public DateTime creation_time { get; set; }
        public PurchaseInvoice purchase_invoice { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EDocumentAction
    {
        create_purchase_invoice,
        get_payment_due_date,
        post_block_purchase_invoice,
        post_purchase_invoice,
        get_purchase_invoice_lines,
        block_purchase_invoice_for_payment,
        unblock_purchase_invoice_for_payment,
        match_purchase_order
    }
}
