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
            ID = Encryption.AESHelper.EncryptString(ID);
            Alias = Encryption.AESHelper.EncryptString(Alias);
            Path = Encryption.AESHelper.EncryptString(Path);
            //Size
        }

    }
}