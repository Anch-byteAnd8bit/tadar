using Newtonsoft.Json;

namespace nsAPI.Entities
{
    class IdentifierTheory
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("id_topic")]
        public string id_topic { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void EncryptByAES()
        {
            this.ID = Encryption.AESHelper.EncryptString(this.ID);
            this.id_topic = Encryption.AESHelper.EncryptString(this.id_topic);
        }
        /// <summary>
        /// РасШифровка алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void DecryptByAES()
        {
            this.ID = Encryption.AESHelper.DecryptString(this.ID);
            this.id_topic = Encryption.AESHelper.DecryptString(this.id_topic);
        }
        public static IdentifierTheory FromJson(string json) => JsonConvert.DeserializeObject<IdentifierTheory>(json, Converter.Settings);
    }
}
