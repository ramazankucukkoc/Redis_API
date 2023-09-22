using Newtonsoft.Json;

namespace Redis_API.Extensions
{
    public static class StringExtension
    {
        public static T ToObject<T>(this string value) where T : class
        {
            return string.IsNullOrEmpty(value)? null: JsonConvert.DeserializeObject<T>(value);
        }
    }
}
