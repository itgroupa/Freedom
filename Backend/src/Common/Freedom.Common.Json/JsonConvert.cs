using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Freedom.Common.Json;

public static class JsonConvert
{
    public static string GetJsonObj<T>(T? obj) where T : class
    {
        if (obj == null) return string.Empty;

        return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented,
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }

    public static T? GetObjFromJson<T>(string? str) where T : class
    {
        return string.IsNullOrEmpty(str)
            ? null
            : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }
}
