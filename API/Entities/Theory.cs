using Newtonsoft.Json;

namespace nsAPI.Entities
{
    public class Topic
    {
        /// <summary>
        /// Заполняется автоматически
        /// </summary>
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }

        public void Decrypt()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            Name = Encryption.AESHelper.DecryptString(Name);
        }

        public void Encrypt()
        {
            ID = Encryption.AESHelper.EncryptString(ID);
            Name = Encryption.AESHelper.EncryptString(Name);
        }
    }

    public class Theory
    {
        /// <summary>
        /// Заполняется автоматически
        /// </summary>
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("id_class")]
        public string id_class { get; set; }
        [JsonProperty("source")]
        public string source { get; set; }
        [JsonProperty("content")]
        public string content { get; set; }
        [JsonProperty("topic")]
        public Topic topic { get; set; }

        public Theory Clone()
        {
            return new Theory
            {
                ID = ID,
                topic = new Topic
                {
                    ID = topic.ID,
                    Name = topic.Name,
                },
                content = content,
                id_class = id_class,
                source = source,
            };
        }
        /// <summary>
        /// Расшифровка.
        /// </summary>
        public void Decrypt()
        {
            ID = Encryption.AESHelper.DecryptString(ID);
            id_class = Encryption.AESHelper.DecryptString(id_class);
            source = Encryption.AESHelper.DecryptString(source);
            content = Encryption.AESHelper.DecryptString(content);
            topic.Decrypt();
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
            id_class = Encryption.AESHelper.EncryptString(id_class);
            source = Encryption.AESHelper.EncryptString(source);
            content = Encryption.AESHelper.EncryptString(content);
            topic.Encrypt();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }
    }

}
