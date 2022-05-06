using Newtonsoft.Json;

namespace nsAPI.Entities
{
    public class Refbook
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
