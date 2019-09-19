using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blue10SDK
{
    public class AdministrationAction
    {
        public Guid id { get; set; }
        public string id_company { get; set; }
        public EAdministrationAction action { get; set; }
        public DateTime creation_time { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EAdministrationAction
    {
        update_all_master_data,
        update_vendors
    }
}
