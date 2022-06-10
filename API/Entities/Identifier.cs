using Newtonsoft.Json;

namespace nsAPI.Entities
{
    class Identifier
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void EncryptByAES()
        {
            this.ID = Encryption.AESHelper.EncryptStringB64(this.ID);
        }
        /// <summary>
        /// РасШифровка алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void DecryptByAES()
        {
            this.ID = Encryption.AESHelper.DecryptStringB64(this.ID);
        }
        public static Identifier FromJson(string json) => JsonConvert.DeserializeObject<Identifier>(json, Converter.Settings);
    }
}
