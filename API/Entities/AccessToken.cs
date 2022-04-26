using Newtonsoft.Json;

namespace nsAPI.Entities
{
    /// <summary>
    /// Ответ с access_token.
    /// </summary>
    class AccessToken
    {
        /// <summary>
        /// Ключ доступа.
        /// </summary>
        [JsonProperty("secure_key")]
        public string Token{ get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        [JsonProperty("user_id")]
        public string UserID { get; set; }

        /// <summary>
        /// Расшифровка токена алгоритмом AES с имеющимися ключами.
        /// </summary>
        public void DecryptByAES()
        {
            this.Token = Encryption.AESHelper.DecryptString(this.Token);
            this.UserID = Encryption.AESHelper.DecryptString(this.UserID);
        }

        /// <summary>
        /// Конвертирование строки в формате JSON в тип AccessToken.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static AccessToken FromJson(string json) => JsonConvert.DeserializeObject<AccessToken>(json, Converter.Settings);
    }
}
