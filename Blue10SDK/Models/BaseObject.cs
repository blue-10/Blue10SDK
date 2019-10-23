using System;

namespace Blue10SDK.Models
{
    public class BaseObject
    {
        public Guid Id { get; set; }

        public string AdministrationCode { get; set; }

        public string IdCompany { get; set; }

        public DateTime LastUpdateDate { get; set; }
    }
}
