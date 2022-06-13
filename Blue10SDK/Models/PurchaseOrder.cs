using System.Collections.Generic;

namespace Blue10SDK.Models
{
    public class PurchaseOrder : BaseObject
    {
        public string VendorCode { get; set; }
        public string Description { get; set; }
        public string OrderedByEmail { get;set;}
        public List<PurchaseOrderLine> OrderLines { get; set; }
    }
}
