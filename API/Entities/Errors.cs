using Newtonsoft.Json;

namespace nsAPI.Entities
{
    public partial class Error
    {
        [JsonProperty("error")]
        public ErrorInfo errorInfo { get; set; }
    }

    public partial class ErrorInfo
    {
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("additional")]
        public string Additional { get; set; }
    }

    public partial class Error
    {
        public static Error FromJson(string json) => JsonConvert.DeserializeObject<Error>(json, Converter.Settings);
    }
}
