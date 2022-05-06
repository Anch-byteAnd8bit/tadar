using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsAPI.Entities
{
    class Test
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("dateTimeStart")]
        public string DateTimeStart { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }


        [JsonProperty("id_Journal")]
        public string id_Journal { get; set; }

        [JsonProperty("DateClose")]
        public string DateClose { get; set; }
    }


    public class TestForReg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id_user")]
        public string id_User { get; set; }

        //[JsonProperty("dateCreate")]
        //public string DateCreate { get; set; }

        [JsonProperty("journal")]
        public JournalForReg Journal { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void EncryptByAES()
        {
            this.Name = Encryption.AESHelper.EncryptString(this.Name);
            this.Description = Encryption.AESHelper.EncryptString(this.Description);
            this.id_User = Encryption.AESHelper.EncryptString(this.id_User);
            this.Journal.EncryptByAES();
        }
        

        /// <summary>
        /// Конвертирование строки в формате JSON в тип ClassForReg.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static TestForReg FromJson(string json) => JsonConvert.DeserializeObject<TestForReg>(json, Converter.Settings);

    }

    public class RegisteredTest
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("dateCreate")]
        public string DateCreate { get; set; }

        [JsonProperty("dateClose")]
        public string DateStart { get; set; }

        [JsonProperty("dateStart")]
        public string DateClose { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void EncryptByAES()
        {
            this.Name = Encryption.AESHelper.EncryptString(this.Name);
            this.Description = Encryption.AESHelper.EncryptString(this.Description);

            this.ID = Encryption.AESHelper.EncryptString(this.ID);
            //this.DateCreate = Encryption.AESHelper.EncryptString(this.DateCreate);
            //this.DateClose = Encryption.AESHelper.EncryptString(this.DateClose);
        }

        /// <summary>
        /// Конвертирование строки в формате JSON в тип ClassForReg.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static RegisteredTest FromJson(string json) => JsonConvert.DeserializeObject<RegisteredTest>(json, Converter.Settings);
    }
}
