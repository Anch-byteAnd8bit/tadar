using Newtonsoft.Json;

namespace nsAPI.Entities
{
    public class Refbook
    {
        /// <summary>
        /// Заполняется автоматически
        /// </summary>
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
