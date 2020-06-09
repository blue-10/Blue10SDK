namespace Blue10SDK.Models
{
    public class InvoiceLine
    {
        public int LineNo { get; set; }

        public string GlAccountCode { get; set; } = string.Empty;

        public decimal NetAmount { get; set; }

        public decimal VatAmount { get; set; }

        public string Description { get; set; } = string.Empty;

        public string VatCode { get; set; }

        public bool VatReverseCharged { get; set; }

        public string CostCenterCode { get; set; }

        public string CostUnitCode { get; set; }

        public string ProjectCode { get; set; }

        public string FreeField1 { get; set; }

        public string FreeField2 { get; set; }
        public string ArticleCode { get; set; }

        public string WarehouseCode { get; set; }
        public string PurchaseOrderCode { get; set; }

        public string PurchaseOrderLineCode { get; set; }
    }
}
