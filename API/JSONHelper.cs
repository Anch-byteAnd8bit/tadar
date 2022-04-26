using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace nsAPI.JSON
{
    public static class JSONHelper
    {

        public static bool IsError(string JSONstring)
        {
            if (IsValidJson(JSONstring))
            {
                // Конвертируем строку JSON в объект JObject
                JObject lvl1 = JObject.Parse(JSONstring);
                // Если ключ - "error"
                return lvl1.ContainsKey("error");
            }
            else
            {
                return false;
            }
        }
        public static bool IsResponse(string JSONstring)
        {
            if (IsValidJson(JSONstring))
            {
                // Конвертируем строку JSON в объект JObject
                JObject lvl1 = JObject.Parse(JSONstring);
                // Если ключ - "error"
                return lvl1.ContainsKey("response");
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Проверяет является ли строка строкой в формате JSON.
        /// </summary>
        public static bool IsValidJson(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }
            var value = stringValue.Trim();
            if ((value.StartsWith("{") && value.EndsWith("}")) || //For object
                (value.StartsWith("[") && value.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
