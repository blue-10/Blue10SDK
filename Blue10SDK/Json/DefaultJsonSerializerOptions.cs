using System.Text.Json;

namespace Blue10SDK.Json
{
    public sealed class DefaultJsonSerializerOptions
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = new Blue10JsonNamingPolicy()
        };

        // These settings were explicitly set for NewtonSoft.Json. Not sure if we need them
        // TypeNameHandling = TypeNameHandling.Auto, // for Array of basetype to deserialize polymorphic types..
        // ObjectCreationHandling = ObjectCreationHandling.Replace,
        // DateTimeZoneHandling = DateTimeZoneHandling.Utc,

    }
}