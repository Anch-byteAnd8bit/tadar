﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace nsAPI.Entities
{
    public class Response
    {
        [JsonProperty("response")]
        public List<JObject> data { get; set; }
        //public Dictionary<string, object> data { get; set; }
        /// <summary>
        /// Получить объект Response из JSON-строки.
        /// </summary>
        /// <param name="json">Строка в формате JSON</param>
        /// <returns></returns>
        public static Response FromJson(string json) => JsonConvert.DeserializeObject<Response> (json, Converter.Settings);
    }
}

