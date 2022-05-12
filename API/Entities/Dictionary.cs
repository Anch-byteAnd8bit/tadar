using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption;

namespace nsAPI.Entities
{
    public class Word
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("HakWord")]
        public string HakWord { get; set; }

        [JsonProperty("RusWord")]
        public string RusWord { get; set; }

        /// <summary>
        /// 1 - Существительное, 2 - Прилагательное, 3 - Глагол
        /// </summary>
        [JsonProperty("id_TypeWord")]
        public string id_TypeWord { get; set; }

        /// <summary>
        /// Если слово из общего словаря, то это свойство должно быть равно null.
        /// </summary>
        [JsonProperty("id_User")]
        public string id_User { get; set; }

        /// <summary>
        /// Шифрование.
        /// </summary>
        public void Encrypte()
        {
            ID = AESHelper.EncryptString(ID);
            HakWord = AESHelper.EncryptString(HakWord);
            RusWord = AESHelper.EncryptString(RusWord);
            id_TypeWord = AESHelper.EncryptString(id_TypeWord);
            id_User = AESHelper.EncryptString(id_User);
        }

        /// <summary>
        /// Расшифровка.
        /// </summary>
        public void Decrypte()
        {
            ID = AESHelper.DecryptString(ID);
            HakWord = AESHelper.DecryptString(HakWord);
            RusWord = AESHelper.DecryptString(RusWord);
            id_TypeWord = AESHelper.DecryptString(id_TypeWord);
            id_User = AESHelper.DecryptString(id_User);
        }

        public static Word FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Word>(json, Converter.Settings);
        }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Converter.Settings);
        }
    }
}
