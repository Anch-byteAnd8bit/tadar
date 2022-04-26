using Newtonsoft.Json;

namespace nsAPI.Entities
{
    class IdentifierClassroom
    {
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("id_Journal")]
        public string id_Journal { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void EncryptByAES()
        {
            this.ID = Encryption.AESHelper.EncryptString(this.ID);
            this.id_Journal = Encryption.AESHelper.EncryptString(this.id_Journal);
        }
        /// <summary>
        /// РасШифровка алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void DecryptByAES()
        {
            this.ID = Encryption.AESHelper.DecryptString(this.ID);
            this.id_Journal = Encryption.AESHelper.DecryptString(this.id_Journal);
        }
        public static IdentifierClassroom FromJson(string json) => JsonConvert.DeserializeObject<IdentifierClassroom>(json, Converter.Settings);
    }
}
