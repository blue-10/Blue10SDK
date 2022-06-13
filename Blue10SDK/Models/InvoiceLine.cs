using System;

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
        public string Dimension3Code { get; set; }
        public string Dimension4Code { get; set; }
        public string Dimension5Code { get; set; }
        public string ProjectCode { get; set; }

        public string FreeField_1 { get; set; }

        public string FreeField_2 { get; set; }
        public string FreeField_3 { get; set; }
        public string FreeField_4 { get; set; }
        public string FreeField_5 { get; set; }
        public string ArticleCode { get; set; }

        public string WarehouseCode { get; set; }
        public string PurchaseOrderCode { get; set; }

        public string PurchaseOrderLineCode { get; set; }
        public DateTime? DeferredFromDate { get; set; }
        public DateTime? DeferredToDate { get; set; }
    }
}
