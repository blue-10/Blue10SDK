using System;

namespace Blue10SDK.Models
{
    public class LogisticsDocumentAction
    {
        public Guid Id { get; set; }

        public string Action { get; set; }

        public string Status { get; set; }

        public string Result { get; set; }

        public string Message { get; set; }

        public DateTime CreationTime { get; set; }

        public PurchaseInvoice PurchaseInvoice { get; set; }
    }
}
