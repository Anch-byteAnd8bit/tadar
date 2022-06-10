using Newtonsoft.Json;

namespace nsAPI.Entities
{
    class Classroom
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
    }


    public class ClassroomForReg
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("id_User")]
        public string id_User { get; set; }

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
        }

        /// <summary>
        /// Конвертирование строки в формате JSON в тип ClassForReg.
        /// </summary>
        /// <param Name="json"></param>
        /// <returns></returns>
        public static RegisteredClassroom FromJson(string json) => JsonConvert.DeserializeObject<RegisteredClassroom>(json, Converter.Settings);
    }
}
