using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDK
{
    public class PurchaseInvoice
    {
        public Guid id { get; set; }
        public string administration_code { get; set; }
        public string blue10_code { get; set; }
        public string id_company { get; set; }
        public string vendor_code { get; set; }
        public string invoice_number { get; set; }
        public string header_description { get; set; }
        public string invoice_type { get; set; }
        public DateTime invoice_date { get; set; }
        public DateTime posting_date { get; set; }
        public decimal gross_amount { get; set; }
        public decimal net_amount { get; set; }
        public string currency_code { get; set; }
        public string payment_term_code { get; set; }
        public string payment_reference { get; set; }
        public DateTime? payment_due_date { get; set; }
        public DateTime? payment_date { get; set; }
        public string free_field_1 { get; set; }
        public string free_field_2 { get; set; }
        public bool blocked_for_payment { get; set; }
        public string document_url { get; set; }
        public string purchase_order_number { get; set; }
        public string vat_scenario_code { get; set; }
        public List<InvoiceLine> invoice_lines { get; set; }
        public List<InvoiceVatLine> invoice_vat_lines { get; set; }
    }
}
