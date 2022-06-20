using Newtonsoft.Json;
using System;

namespace nsAPI.Entities
{
    public class Classroom
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DateTimeCreate")]
        public string DateTimeCreate { get; set; }

        [JsonProperty("DateTimeClose")]
        public string DateTimeClose { get; set; }
        [JsonProperty("Passkey")]
        public string Passkey { get; set; }

        internal Classroom GetClone()
        {
            return new Classroom
            {
                ID = this.ID,
                Name = this.Name,
                Description = this.Description,
                DateTimeCreate = this.DateTimeCreate,
                DateTimeClose = this.DateTimeClose,
                Passkey = this.Passkey,

            };
        }

        internal void Encrypt()
        {
            this.ID = Encryption.AESHelper.EncryptStringB64(this.ID);
            this.Name = Encryption.AESHelper.EncryptStringB64(this.Name);
            this.DateTimeCreate = Encryption.AESHelper.EncryptStringB64(this.DateTimeCreate);
            this.DateTimeClose = Encryption.AESHelper.EncryptStringB64(this.DateTimeClose);
            this.Description = Encryption.AESHelper.EncryptStringB64(this.Description);
            this.Passkey = Encryption.AESHelper.EncryptStringB64(this.Passkey);
        }
    }


    public class ClassroomForReg
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("id_User")]
        public string id_User { get; set; }
        [JsonProperty("Passkey")]
        public string Passkey { get; set; }

        //[JsonProperty("DateTimeCreate")]
        //public string DateTimeCreate { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void EncryptByAES()
        {
            this.Name = Encryption.AESHelper.EncryptStringB64(this.Name);
            this.Description = Encryption.AESHelper.EncryptStringB64(this.Description);
            this.id_User = Encryption.AESHelper.EncryptStringB64(this.id_User);
            this.Passkey = Encryption.AESHelper.EncryptStringB64(this.Passkey);
        }
        

        /// <summary>
        /// Конвертирование строки в формате JSON в тип ClassForReg.
        /// </summary>
        /// <param Name="json"></param>
        /// <returns></returns>
        public static ClassroomForReg FromJson(string json) => JsonConvert.DeserializeObject<ClassroomForReg>(json, Converter.Settings);

    }


    public class RegisteredClassroom
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DateTimeCreate")]
        public string DateTimeCreate { get; set; }

        [JsonProperty("DateTimeClose")]
        public string DateTimeClose { get; set; }

        [JsonProperty("Passkey")]
        public string Passkey { get; set; }

        /// <summary>
        /// Шифрование алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void DecryptByAES()
        {
            this.Name = Encryption.AESHelper.DecryptStringB64(this.Name);
            this.Description = Encryption.AESHelper.DecryptStringB64(this.Description);
            this.ID = Encryption.AESHelper.DecryptStringB64(this.ID);
            this.DateTimeCreate = Encryption.AESHelper.DecryptStringB64(this.DateTimeCreate);
            this.DateTimeClose = Encryption.AESHelper.DecryptStringB64(this.DateTimeClose);
            this.Passkey = Encryption.AESHelper.DecryptStringB64(this.Passkey);
        }

        /// <summary>
        /// Конвертирование строки в формате JSON в тип ClassForReg.
        /// </summary>
        /// <param Name="json"></param>
        /// <returns></returns>
        public static RegisteredClassroom FromJson(string json) => JsonConvert.DeserializeObject<RegisteredClassroom>(json, Converter.Settings);
    }
}
