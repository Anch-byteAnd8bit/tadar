using Encryption;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace nsAPI.Entities
{

    public class RegisteredUser
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("middlename")]
        public string Middlename { get; set; }

        [JsonProperty("id_gender")]
        public string GenderID { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        //[JsonProperty("pass")]
        //public string Pass { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id_state")]
        public string StateID { get; set; }

        [JsonProperty("bdate")]
        public DateTimeOffset BDate { get; set; }

        [JsonProperty("datereg")]
        public DateTimeOffset DateReg { get; set; }


        /// <summary>
        /// Расшифровывает все данные этого пользователя
        /// через класс AESHelper с текущими ключами.
        /// </summary>
        /// <returns></returns>
        public void DecryptDataByAES()
        {
            if (this == null) return;
            // Дата не шифруется на сервере.
            //Bdate = this.Bdate;
            //DateReg = this.DateReg;
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
    }


    public class UserForRegistration
    {
        public UserForRegistration() { }

        public UserForRegistration(RegisteredUser user)
        {
            this.BDate = user.BDate.ToString("d");
            //this.DateReg = user.Data.DateReg.ToString("d");
            this.Email = user.Email;
            this.GenderID = user.GenderID;
            this.Login = user.Login;
            this.Middlename = user.Middlename;
            this.Name = user.Name;
            this.Pass = null;
            this.Surname = user.Surname;
        }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("middlename")]
        public string Middlename { get; set; }

        /// <summary>
        /// Варианты: "Мужской", "Женский".
        /// </summary>
        [JsonProperty("id_gender")]
        public string GenderID { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("pass")]
        public string Pass { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("bdate")]
        public string BDate { get; set; }

        /*[JsonProperty("datereg")]
        public string DateReg { get; set; }*/

        /// <summary>
        /// Шифрует все данные этого пользователя
        /// через класс AESHelper с текущими ключами.
        /// </summary>
        /// <returns></returns>
        public UserForRegistration GetEncryptedDataByAES()
        {
            UserForRegistration encUser = new UserForRegistration();
            encUser.BDate = AESHelper.EncryptString(this.BDate);
            //encUser.DateReg = AESHelper.EncryptString(this.DateReg);
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
                //DateReg = this.DateReg,

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
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("pass")]
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
