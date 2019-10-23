namespace Blue10SDK.Models
{
    public class VatCode : BaseObject
    {
        public string Name { get; set; }

        public decimal Percentage { get; set; } = 0;

        public bool Inclusive { get; set; }

        public bool ReverseCharge { get; set; }
    }
}
