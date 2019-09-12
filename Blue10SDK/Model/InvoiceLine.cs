namespace Blue10SDK
{
    public class InvoiceLine
    {
        public int line_no { get; set; }
        public string gl_account_code { get; set; } = string.Empty;
        public decimal net_amount { get; set; }
        public decimal vat_amount { get; set; }
        public string description { get; set; } = string.Empty;
        public string vat_code { get; set; }
        public bool vat_reverse_charged { get; set; }
        public string cost_center_code { get; set; }
        public string cost_unit_code { get; set; }
        public string project_code { get; set; }
        public string free_field_1 { get; set; }
        public string free_field_2 { get; set; }
    }
}
