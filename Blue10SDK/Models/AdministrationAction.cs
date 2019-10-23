using System;

namespace Blue10SDK.Models
{
    public class AdministrationAction
    {
        public Guid Id { get; set; }

        public string IdCompany { get; set; }

        public EAdministrationAction Action { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
