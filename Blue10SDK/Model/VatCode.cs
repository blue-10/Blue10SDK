namespace Blue10SDK
{
    public class VatCode : BaseObject
    {
        public string name { get; set; }
        public decimal percentage { get; set; } = 0;
        public bool inclusive { get; set; }
        public bool reverse_charge { get; set; }
    }
}
