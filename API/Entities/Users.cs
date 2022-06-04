using Encryption;
using Newtonsoft.Json;
using nsAPI.Helpers;
using System;
using System.Collections.Generic;

namespace nsAPI.Entities
{

    public class RegisteredUser
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("Surname")]
        public string Surname { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Middlename")]
        public string Middlename { get; set; }

        [JsonProperty("id_Gender")]
        public string GenderID { get; set; }

        [JsonProperty("Login")]
        public string Login { get; set; }

        //[JsonProperty("Pass")]
        //public string Pass { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("id_State")]
        public string StateID { get; set; }

        [JsonProperty("BDate")]
        public string BDate { get; set; }

        [JsonProperty("DateTimeReg")]
        public string DateTimeReg { get; set; }


        /// <summary>
        /// Расшифровывает все данные этого пользователя
        /// через класс AESHelper с текущими ключами.
        /// </summary>
        /// <returns></returns>
        public void DecryptDataByAES()
        {
            if (this == null) return;
            // Дата не шифруется на сервере.
            //BDate = this.BDate;
            //DateTimeReg = this.DateTimeReg;
            ID = AESHelper.DecryptString(this.ID);
            Email = AESHelper.DecryptString(this.Email);
            GenderID = AESHelper.DecryptString(this.GenderID);
            Login = AESHelper.DecryptString(this.Login);
            Middlename = AESHelper.DecryptString(this.Middlename);
            Name = AESHelper.DecryptString(this.Name);
            //Pass = AESHelper.DecryptString(this.Pass);
            Surname = AESHelper.DecryptString(this.Surname);
            StateID = AESHelper.DecryptString(this.StateID);
        }
        /// <summary>
        /// Полное имя в формате "Фамилия Имя Отчество"
        /// </summary>
        public string FullName
        {
            get
            {
                return Surname + " " + Name + " " + Middlename;
            }
        }

        public string Age
        {
            get
            {
                return Other.CalcAgeByBDate(DateTime.Parse(BDate)).ToString();
            }
        }
    }


    public class UserForRegistration
    {
        public UserForRegistration() { }

        public UserForRegistration(RegisteredUser user)
        {
            this.BDate = user.BDate;
            //this.DateTimeReg = user.Data.DateTimeReg.ToString("d");
            this.Email = user.Email;
            this.GenderID = user.GenderID;
            this.Login = user.Login;
            this.Middlename = user.Middlename;
            this.Name = user.Name;
            this.Pass = null;
            this.Surname = user.Surname;
        }

        [JsonProperty("Surname")]
        public string Surname { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Middlename")]
        public string Middlename { get; set; }

        /// <summary>
        /// Варианты: "Мужской", "Женский".
        /// </summary>
        [JsonProperty("id_Gender")]
        public string GenderID { get; set; }

        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonProperty("Pass")]
        public string Pass { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("BDate")]
        public string BDate { get; set; }

        /*[JsonProperty("DateTimeReg")]
        public string DateTimeReg { get; set; }*/

        /// <summary>
        /// Шифрует все данные этого пользователя
        /// через класс AESHelper с текущими ключами.
        /// </summary>
        /// <returns></returns>
        public UserForRegistration GetEncryptedDataByAES()
        {
            UserForRegistration encUser = new UserForRegistration();
            encUser.BDate = AESHelper.EncryptString(this.BDate);
            //encUser.DateTimeReg = AESHelper.EncryptString(this.DateTimeReg);
            encUser.Email = AESHelper.EncryptString(this.Email);
            encUser.GenderID = AESHelper.EncryptString(this.GenderID);
            encUser.Login = AESHelper.EncryptString(this.Login);
            encUser.Middlename = AESHelper.EncryptString(this.Middlename);
            encUser.Name = AESHelper.EncryptString(this.Name);
            encUser.Pass = AESHelper.EncryptString(this.Pass);
            encUser.Surname = AESHelper.EncryptString(this.Surname);
            return encUser;
        }

        /// <summary>
        /// Расшифровывает все данные этого пользователя
        /// через класс AESHelper с текущими ключами.
        /// </summary>
        /// <returns></returns>
        public UserForRegistration GetDecryptedDataByAES()
        {
            return new UserForRegistration
            {
                // Дата не шифруется при отправке на сервер.
                BDate = this.BDate,
                //DateTimeReg = this.DateTimeReg,

                Email = AESHelper.DecryptString(this.Email),
                GenderID = AESHelper.DecryptString(this.GenderID),
                Login = AESHelper.DecryptString(this.Login),
                Middlename = AESHelper.DecryptString(this.Middlename),
                Name = AESHelper.DecryptString(this.Name),
                Pass = AESHelper.DecryptString(this.Pass),
                Surname = AESHelper.DecryptString(this.Surname)
            };
        }

        /// <summary>
        /// Возвращает объект класса пользователя, с данными для авторизации, созданного
        /// из данного объекта класса.
        /// </summary>
        [JsonIgnore()] // Пометка, что это свойства не надо сериализовать.
        public UserForAuthorization UserForAuthorization
        {
            get
            {
                return new UserForAuthorization
                {
                    Login = this.Login,
                    Pass = this.Pass
                };
            }

        }
    }

    /// <summary>
    /// Пользователь для авторизации.
    /// </summary>
    public class UserForAuthorization
    {
        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonProperty("Pass")]
        public string Pass { get; set; }

        /// <summary>
        /// Расшифровывает все данные пользователя для 
        /// авторизации через класс AESHelper с текущими ключами.
        /// </summary>
        /// <returns></returns>
        public UserForAuthorization GetDecryptedDataByAES()
        {
            return new UserForAuthorization
            {
                Login = AESHelper.DecryptString(this.Login),
                Pass = AESHelper.DecryptString(this.Pass)
            };
        }

        /// <summary>
        /// Шифрует все данные пользователя для авторизации
        /// через класс AESHelper с текущими ключами.
        /// </summary>
        /// <returns></returns>
        public UserForAuthorization GetEncryptedDataByAES()
        {
            UserForAuthorization encUser = new UserForAuthorization();
            encUser.Login = AESHelper.EncryptString(this.Login);
            encUser.Pass = AESHelper.EncryptString(this.Pass);
            return encUser;
        }
    }
}
