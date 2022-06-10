using Newtonsoft.Json;

namespace nsAPI.Entities
{

    /// <summary>
    /// Данные изображения
    /// </summary>
    public class Pic
    {
        [JsonProperty("ID")]
        public string ID { get; set; }
        [JsonProperty("Alias")]
        public string Alias { get; set; }
        [JsonProperty("Path")]
        public string Path { get; set; }
        [JsonProperty("Size")]
        public long Size { get; set; }

        /// <summary>
        ///шифровка.
        /// </summary>
        public void EncryptBodyByAES()
        {
            ID = Encryption.AESHelper.EncryptStringB64(ID);
            Alias = Encryption.AESHelper.EncryptStringB64(Alias);
            Path = Encryption.AESHelper.EncryptStringB64(Path);
            //Size
        }

    }
}