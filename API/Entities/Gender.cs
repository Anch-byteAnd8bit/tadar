using Newtonsoft.Json;

namespace nsAPI.Entities
{
    public class Gender
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("gender")]
        public string Name { get; set; }
    }
}
