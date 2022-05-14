using Helpers;
using Newtonsoft.Json;
using nsAPI.Entities;
using nsAPI.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nsAPI.Methods
{

    class MUsers : Basic
    {
        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="user">Данные регистрации.</param>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <returns>AccessToken - при успешной регистрации.</returns>
        public async Task<AccessToken> RegAsync(UserForRegistration user)
        {
            // Публичный ключ сервера.
            //rsa.YourXMLPublicKey = spu;

            // Получаем пользователя с зашифрованными данными.
            UserForRegistration encUser = user.GetEncryptedDataByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            var userJson = encUser.ToJson();

            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var responseJson = await httpPostJSONAsync("users.reg/", userJson);
            if (responseJson.data != null)
            {
                // Конвертируем данные из нулевой ячейки массива ответа в тип AccessToken.
                var decToken = AccessToken.FromJson(responseJson.data[0].ToString());
                // Расшифровываем токен.
                decToken.DecryptByAES();
                // Успех!
                return decToken;
            }
            else
            {
                if (responseJson.Exception.TypeError == TError.DefinedError)
                {
                    if (responseJson.Exception.Code == CODE_ERROR.ERR_UserAlreadyReg)
                    {

                    }
                }
                return null;
            }
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="user">Объект с данными для авторизации пользователя</param>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <returns>True - при успешной авторизации</returns>
        public async Task<AccessToken> AuthAsync(UserForAuthorization user)
        {
            // Публичный ключ сервера.
            //rsa.YourXMLPublicKey = spu;
            // Получаем пользователя с зашифрованными данными.
            UserForAuthorization enсUser = user.GetEncryptedDataByAES();
            // Конвертируем объект в строку в формате JSON.
            string userJson = Serialize.ToJson(enсUser);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("users.auth/", userJson);
            if (httpResponse.data != null)
            {
                var accessToken = AccessToken.FromJson(httpResponse.data[0].ToString());
                // Расшифровываем токен.
                accessToken.DecryptByAES();
                // Успех!
                return accessToken;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Строка логина</param>
        /// <param name="pass">Строка пароля</param>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <returns>true - при успешной авторизации.</returns>
        public async Task<AccessToken> AuthAsync(string login, string pass)
        {
            return await AuthAsync(new UserForAuthorization
            {
                Login = login,
                Pass = pass
            });
        }

        /// <summary>
        /// Получает подробную информацию об указанном количестве пользователей.
        /// </summary>
        /// <param name="api_token">Ключ доступа для работы с сервером.</param>
        /// <param name="searchName"> </param>
        /// <param name="searchMName"> </param>
        /// <param name="searchSurname"> </param>
        /// <param name="settings">Настройки поиска - кол-во пользователей (макс: 50) и 
        /// смещение относительно первого найденного.</param>
        /// <returns>Информация о пользователях.</returns>
        public async Task<List<RegisteredUser>> FindAsync(string api_token, SettingsFind settings, string searchName = null, string searchMName = null, string searchSurname = null)
        {
            if (settings == null) settings = new SettingsFind();
            if (settings.Count <= 0 || settings.Shift < 0)
            {
                return null;
            }
            // Если задано слишком большое кол-во.
            settings.Count = Math.Min(settings.Count, 50);
             
            var d = new Dictionary<string, string>();
            d.Add("name", searchName);
            d.Add("middlename", searchMName);
            d.Add("surname", searchSurname);
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            urlParam["secure_key"] = api_token;
            // Кол-во пользователей.
            urlParam["count"] = settings.Count.ToString();
            // Смещение относительно первого пользователя.
            urlParam["shift"] = settings.Shift.ToString();
            // 
            string JSONSearch = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("users.find/", JSONSearch, urlParam);
            if (httpResponse.data != null)
            {
                // Возвращаем список пользователей.
                List<RegisteredUser> registeredUsers = new List<RegisteredUser>();
                // Конвертируем юзеров из JSON в известный нам тип RegisteredUser.
                httpResponse.data.ForEach(el =>
                {
                    registeredUsers.Add(JsonConvert.DeserializeObject<RegisteredUser>(el.ToString()));
                });
                // Расшифровываем данные пользователя.
                registeredUsers.ForEach(u => u.DecryptDataByAES());
                // Возвращаем список пользователей.
                return registeredUsers;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получает подробную информацию о пользоателях с заданными ID.
        /// </summary>
        /// <param name="userIds">ID пользователей.</param>
        /// <returns>Информация о пользователях.</returns>
        public async Task<List<RegisteredUser>> ByIdAsync(string api_token, string[] userIds)
        {
            if (userIds == null || userIds.Count() <= 0)
            {
                return null;
            }
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // Добавление в массив по ключу "ids", список идентификаторов пользователей.
            d.Add("ids", userIds);
            // Сериализация (конвертирование в формат JSON).
            string usersJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("users.get/", usersJSON, urlParam);
            if (httpResponse.data != null)
            {
                // Возвращаем список пользователей.
                List<RegisteredUser> registeredUsers = new List<RegisteredUser>();
                //
                httpResponse.data.ForEach(el =>
                {
                    registeredUsers.Add(JsonConvert.DeserializeObject<RegisteredUser>(el.ToString()));
                });
                // Расшифровываем данные пользователя.
                registeredUsers.ForEach(u => u.DecryptDataByAES());
                // Возвращаем список пользователей.
                return registeredUsers;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получение информации о пользователе с заданным ID
        /// </summary>
        public async Task<RegisteredUser> ByIdAsync(string api_token, string userId)
        {
            // Засовываем идентификатор пользователя в массив, чтобы отправить
            // его в функцию получения списка юзеров.
            string[] userIds = { userId };
            // Возвращаем список пользователей.
            List<RegisteredUser> registeredUsers = await ByIdAsync(api_token, userIds);
            // Возвращаем пользователя.
            return registeredUsers?[0];
        }

        /// <summary>
        /// Возвращает список пользователей из указанного класса.
        /// </summary>
        /// <param name="api_token"></param>
        /// <param name="id_Class">Идентификатор класса.</param>
        /// <returns>Список пользователей из указанного класса</returns>
        public async Task<List<RegisteredUser>> ByClassIdAsync(string api_token, string id_Class)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // Добавление в массив по ключу "id", идентификатора класса.
            urlParam.Add("id_Class", id_Class);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("users.byClass/", urlParam);
            if (httpResponse.data != null)
            {
                // Возвращаем список классов.
                List<RegisteredUser> registeredUsers = new List<RegisteredUser>();
                //
                httpResponse.data.ForEach(el =>
                {
                    registeredUsers.Add(JsonConvert.DeserializeObject<RegisteredUser>(el.ToString()));
                });
                // Расшифровываем данные класса.
                registeredUsers.ForEach(u => u.DecryptDataByAES());
                // Возвращаем список классов.
                return registeredUsers;
            }
            else
            {
                return null;
            }
        }
    }
}
