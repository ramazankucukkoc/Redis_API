using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Redis_API.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
