using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encryption;
using System.IO;
using Helpers;

namespace nsAPI.Entities
{
    public class Word : ICloneable
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
        /// Шифрование. Если задан путь к аудиоайлу, то он сериализуется в свойство "Content".
        /// </summary>
        public void EncrypteAndSerializeContent()
        {
            ID = AESHelper.EncryptStringB64(ID);
            HakWord = AESHelper.EncryptStringB64(HakWord);
            RusWord = AESHelper.EncryptStringB64(RusWord);
            id_TypeWord = AESHelper.EncryptStringB64(id_TypeWord);
            id_User = AESHelper.EncryptStringB64(id_User);

            SerializeContent();
        }

        /// <summary>
        /// Расшифровка. Если приходит файл, то он тоже расшифровыается, в PathToAudio записывается путь к этому файлу.
        /// </summary>
        public void DecrypteAndDeserializeContent()
        {
            ID = AESHelper.DecryptStringB64(ID);
            HakWord = AESHelper.DecryptStringB64(HakWord);
            RusWord = AESHelper.DecryptStringB64(RusWord);
            id_TypeWord = AESHelper.DecryptStringB64(id_TypeWord);
            id_User = AESHelper.DecryptStringB64(id_User);

            DeserializeContent();
        }

        public void SerializeContent()
        {
            if (!string.IsNullOrWhiteSpace(PathAudio))
            {
                // Для работы TripleDES требуется вектор инициализации (IV) и ключ (Key)
                // Операции шифрования/деширования должны использовать одинаковые значения IV и Key
                using (var inputStream = File.OpenRead(PathAudio))
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.SetLength(0);
                    inputStream.CopyTo(ms);
                    Content = Convert.ToBase64String(ms.ToArray());
                }
                PathAudio = null;
            }
            else
            {
                Content = null;
            }
        }

        public void DeserializeContent()
        {
            if (!string.IsNullOrWhiteSpace(Content))
            {
                PathAudio = Path.GetTempPath() + "aud//" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".wav";
                byte[] encryptedByte = Convert.FromBase64String(Content);
                try
                {
                  Directory.CreateDirectory(Path.GetDirectoryName(PathAudio));
                using (var inputStream = new MemoryStream(encryptedByte))
                using (var outputStream = new FileStream(PathAudio, FileMode.Create, FileAccess.Write))
                {
                    inputStream.CopyTo(outputStream);
                }
                }
                catch (Exception x)
                {
                    Msg.Write(x.Message);
                    throw;
                }
               
                var exs = File.Exists(PathAudio);
                Content = null;
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

        public Word GetClone()
        {
            return (Word)this.Clone();
        }

        public object Clone()
        {
            Word clone = new Word();
            clone.ID = ID;
            clone.HakWord = HakWord;
            clone.RusWord = RusWord;
            clone.id_TypeWord = id_TypeWord;
            clone.id_User = id_User;
            clone.Content = Content;
            clone.PathAudio = PathAudio;
            return clone;
        }
    }
}
