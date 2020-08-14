using System;
using System.Collections.Generic;

namespace Blue10SDK.Models
{
    public class PurchaseInvoice
    {
        public Guid Id { get; set; }

        public string AdministrationCode { get; set; }

        public string Blue10Code { get; set; }

        public string IdCompany { get; set; }

        public string VendorCode { get; set; }

        public string InvoiceNumber { get; set; }

        public string HeaderDescription { get; set; }

        public string InvoiceType { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime PostingDate { get; set; }

        public decimal GrossAmount { get; set; }

        public decimal NetAmount { get; set; }

        public string CurrencyCode { get; set; }

        public string PaymentTermCode { get; set; }

        public string PaymentReference { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string FreeField1 { get; set; }

        public string FreeField2 { get; set; }

        public bool BlockedForPayment { get; set; }

        public string DocumentUrl { get; set; }

        public string PurchaseOrderNumber { get; set; }
        public string PackingSlipNumber { get; set; }

        public string VatScenarioCode { get; set; }

        public List<InvoiceLine> InvoiceLines { get; set; }

        public List<InvoiceVatLine> InvoiceVatLines { get; set; }
    }
}
