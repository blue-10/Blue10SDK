using Blue10SDK.Models;

namespace Blue10SDK
{
    public class Article : BaseObject
    {
        public string Name { get; set; }
        public decimal PricePerUnit { get; set; }
        public string DefaultLedgerCode { get; set; }
        public string DefaultWarehouseCode { get; set; }
    }
}
