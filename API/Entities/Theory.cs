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
            ID = Encryption.AESHelper.DecryptString(ID);
            id_Class = Encryption.AESHelper.DecryptString(id_Class);
            Source = Encryption.AESHelper.DecryptString(Source);
            Content = Encryption.AESHelper.DecryptString(Content);
            Topic = Encryption.AESHelper.DecryptString(Topic);
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
            ID = Encryption.AESHelper.EncryptString(ID);
            id_Class = Encryption.AESHelper.EncryptString(id_Class);
            Source = Encryption.AESHelper.EncryptString(Source);
            Content = Encryption.AESHelper.EncryptString(Content);
            Topic = Encryption.AESHelper.EncryptString(Topic);
            
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }
    }

}
