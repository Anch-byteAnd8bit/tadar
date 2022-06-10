using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption;
using System.IO;

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
        /// Аудиофайл со звучанием слова на хакасском языке в формате ".wav".
        /// </summary>
        [JsonProperty("Content")]
        public string Content { get; set; }
        /// <summary>
        /// Путь к аудиофайлу со звучанием слова.
        /// </summary>
        [JsonProperty("PathAudio")]
        public string PathAudio { get; set; }

        /// <summary>
        /// Шифрование. Если задан путь к аудиоайлу, то шифруется и он в свойство "Content".
        /// </summary>
        public void Encrypte()
        {
            ID = AESHelper.EncryptStringB64(ID);
            HakWord = AESHelper.EncryptStringB64(HakWord);
            RusWord = AESHelper.EncryptStringB64(RusWord);
            id_TypeWord = AESHelper.EncryptStringB64(id_TypeWord);
            id_User = AESHelper.EncryptStringB64(id_User);

            if (!string.IsNullOrWhiteSpace(PathAudio))
            {
                Content = AESHelper.EncryptFileToStringB64(PathAudio);
            }
            else
            {
                Content = null;
            }
        }


        /// <summary>
        /// Расшифровка. Если приходит файл, то он тоже расшифровыается, в PathToAudio записывается путь к этому файлу.
        /// </summary>
        public void Decrypte()
        {
            ID = AESHelper.DecryptStringB64(ID);
            HakWord = AESHelper.DecryptStringB64(HakWord);
            RusWord = AESHelper.DecryptStringB64(RusWord);
            id_TypeWord = AESHelper.DecryptStringB64(id_TypeWord);
            id_User = AESHelper.DecryptStringB64(id_User);
            
            if (!string.IsNullOrWhiteSpace(Content))
            {
                PathAudio = Path.GetTempPath() + "aud\\" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".wav";
                AESHelper.DecryptStringB64ToFile(Content, PathAudio);
            }
            else
            {
                Content = null;
            }
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
