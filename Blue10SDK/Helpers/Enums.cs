using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blue10SDK
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EB10Endpoint
    {
        administrationactions,
        companies,
        costcenters,
        costunits,
        documentactions,
        documentoriginals,
        glaccounts,
        logisticsdocumentactions,
        me,
        paymentterms,
        projects,
        purchaseinvoices,
        purchaseorders,
        vatcodes,
        vatscenarios,
        vendors
    }
}
