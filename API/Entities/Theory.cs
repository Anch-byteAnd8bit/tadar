using Newtonsoft.Json;

namespace nsAPI.Entities
{
    public class Theory
    {
        /// <summary>
        /// Заполняется автоматически
        /// </summary>
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("id_Class")]
        public string id_Class { get; set; }
        [JsonProperty("Source")]
        public string Source { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("Topic")]
        public string Topic { get; set; }

        public Theory Clone()
        {
            return new Theory
            {
                ID = ID,
                Topic = Topic,
                Content = Content,
                id_Class = id_Class,
                Source = Source,
            };
        }
        /// <summary>
        /// Расшифровка.
        /// </summary>
        public void Decrypt()
        {
            ID = Encryption.AESHelper.DecryptStringB64(ID);
            id_Class = Encryption.AESHelper.DecryptStringB64(id_Class);
            Source = Encryption.AESHelper.DecryptStringB64(Source);
            Content = Encryption.AESHelper.DecryptStringB64(Content);
            Topic = Encryption.AESHelper.DecryptStringB64(Topic);
        }

        public static Theory FromJson(string json)
        {
            if (json.Contains("Nothing")) return null;
            else return JsonConvert.DeserializeObject<Theory>(json, Converter.Settings);
        }

        /// <summary>
        /// Шифрование.
        /// </summary>
        public void Encrypt()
        {
            ID = Encryption.AESHelper.EncryptStringB64(ID);
            id_Class = Encryption.AESHelper.EncryptStringB64(id_Class);
            Source = Encryption.AESHelper.EncryptStringB64(Source);
            Content = Encryption.AESHelper.EncryptStringB64(Content);
            Topic = Encryption.AESHelper.EncryptStringB64(Topic);
            
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }
    }

}
