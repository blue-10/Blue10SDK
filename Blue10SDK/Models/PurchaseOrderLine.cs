using System;

namespace Blue10SDK.Models
{
    public class PurchaseOrderLine
    {
        public int LineNo { get; set; }
        public string AdministrationCode { get; set; }
        public string Description { get; set; }        
        public decimal NetAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityReceived { get; set; }
        public decimal QuantityInvoiced { get; set; }
        public string ArticleCode { get; set; }
        public string CostCenterCode { get; set; }
        public string CostUnitCode { get; set; }
        public string GlAccountCode { get; set; } 
        public string ProjectCode { get; set; }
        public string VatCode { get; set; }
        public string WarehouseCode { get; set; }
        public string VendorArticleCode { get; set; }
        public DateTime? DateReceived { get; set; }
    }
}
