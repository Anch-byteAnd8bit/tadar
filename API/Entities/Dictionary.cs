using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsAPI.Entities
{
    public class Dict
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("HakWord")]
        public string HakWord { get; set; }

        [JsonProperty("RusWord")]
        public string RusWord { get; set; }

        [JsonProperty("id_TypeWord ")]
        public string id_TypeWord { get; set; }

        /// <summary>
        /// Если слово из общего словаря, то это свойство должно быть равно null.
        /// </summary>
        [JsonProperty("id_User")]
        public string id_User { get; set; }

    }
}
