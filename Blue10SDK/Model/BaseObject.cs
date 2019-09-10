using System;
using System.Collections.Generic;
using System.Text;

namespace Blue10SDK
{
    public class BaseObject
    {
        public Guid id { get; set; }
        public string administration_code { get; set; }
        public string id_company { get; set; }
        public DateTime last_update_date { get; set; }
    }
}
