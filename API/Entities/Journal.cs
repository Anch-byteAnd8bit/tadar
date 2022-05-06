using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsAPI.Entities
{
    /*class Journal
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("dateCreate")]
        public string DateCreate { get; set; }
    }*/

    public class JournalForReg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void EncryptByAES()
        {
            this.Name = Encryption.AESHelper.EncryptString(this.Name);
            this.Description = Encryption.AESHelper.EncryptString(this.Description);
        }

        /// <summary>
        /// Конвертирование строки в формате JSON в тип JournalForReg.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JournalForReg FromJson(string json) => JsonConvert.DeserializeObject<JournalForReg>(json, Converter.Settings);
    }

    public class RegisteredJournal
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("dateCreate")]
        public string DateCreate { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void DecryptByAES()
        {
            this.Name = Encryption.AESHelper.DecryptString(this.Name);
            this.Description = Encryption.AESHelper.DecryptString(this.Description);
            this.ID = Encryption.AESHelper.DecryptString(this.ID);
        }

        /// <summary>
        /// Конвертирование строки в формате JSON в тип JournalForReg.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JournalForReg FromJson(string json) => JsonConvert.DeserializeObject<JournalForReg>(json, Converter.Settings);
    }
}
