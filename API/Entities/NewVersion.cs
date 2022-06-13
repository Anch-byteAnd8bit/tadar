using Newtonsoft.Json;

namespace nsAPI.Entities
{
    public partial class NewVersion
    {
        [JsonProperty("ThereUpdate")]
        public bool ThereUpdate { get; set; }
        [JsonProperty("AppVersion")]
        public string AppVersion { get; set; }
        [JsonProperty("Name")]
        public string NameVersion { get; set; }
    }
}
