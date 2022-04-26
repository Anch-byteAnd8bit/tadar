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
        /// <returns>True - при успешной регистрации.</returns>
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
            // Проверяем не оказался ли результат пустым.
            if (string.IsNullOrEmpty(responseJson))
            {
                return null;
            }
            // Если вернулась ошибка.
            if (JSONHelper.IsError(responseJson))
            {
                // Произошла ошибка при попытке получения информации из БД.
                Error error = Error.FromJson(responseJson);
                // Обработка ошибки.
                _=errProcess(error, TypeMethod.USERS_REG);
                return null;
            }
            else if (JSONHelper.IsResponse(responseJson))
            {
                // Конвертируем строку JSON в тип response.
                Response response = Response.FromJson(responseJson);
                // Конвертируем данные из нулевой ячейки массива ответа в тип AccessToken.
                var decToken = AccessToken.FromJson(response.data[0].ToString());
                //и дешиФРУЕМ
                decToken.DecryptByAES();
                //
                return (decToken);
            }
            // Если полученная строка незнакома.
            else
            {
                Console.WriteLine(responseJson??"null");
                Console.WriteLine("Полученное значение неизвестно." +
                    "\nЗначение скопировано в буфер обмена!");
                return null;
            }
            //return false;
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="user">Объект с данными для авторизации пользователя</param>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <returns>True - при успешной авторизации</returns>
        public bool TryAuth(UserForAuthorization user, out AccessToken accessToken)
        {
            // Публичный ключ сервера.
            //rsa.YourXMLPublicKey = spu;
            // Получаем пользователя с зашифрованными данными.
            UserForAuthorization enсUser = user.GetEncryptedDataByAES();
            // Конвертируем объект в строку в формате JSON.
            string userJson = Serialize.ToJson(enсUser);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            string result = httpPostJSONAsync("users.auth/", userJson).Result;
            // Проверяем не оказался ли результат пустым.
            if (string.IsNullOrEmpty(result))
            {
                accessToken = null;
                return false;
            }
            //Console.WriteLine(result);
            // Если пришла ошибка.
            if (JSONHelper.IsError(result))
            {
                // Произошла ошибка при попытке получения информации из БД.
                // Надо расшифровывать дальше как тип "error".
                Error error = Error.FromJson(result);
                accessToken = null;
                // Проверяем можно ли исправить ошибку.
                return errProcess(error, TypeMethod.USERS_AUTH);
            }
            // Иначе это нормальный отет.
            else if (JSONHelper.IsResponse(result))
            {
                // Конвертируем строку JSON в тип response.
                Response response = Response.FromJson(result);
                // Конвертируем данные из нулевой ячейки массива ответа в тип AccessToken.
                accessToken = AccessToken.FromJson(response.data[0].ToString());
                // Расшифровываем токен.
                accessToken.DecryptByAES();
                // Успех!
                return true;
            }
            // Если полученная строка не знакома.
            else
            {
                Console.WriteLine(result);
                accessToken = null;
                return false; // <<<< Заглушка.
            }
            //return false;
        }


        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login">Строка логина</param>
        /// <param name="pass">Строка пароля</param>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <returns>true - при успешной авторизации.</returns>
        public bool Auth(string login, string pass, out AccessToken accessToken)
        {
            return TryAuth(new UserForAuthorization
            {
                Login = login,
                Pass = pass
            }, out accessToken);
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
        public List<RegisteredUser> Find(string api_token, string searchName = null, string searchMName = null, string searchSurname = null, SettingsFind settings = null)
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
            string result = httpPostJSONAsync("users.find/", JSONSearch, urlParam).Result;
            // Если полученный ответ это точно Json и точно объект или массив
            if (JSONHelper.IsError(result))
            {
                // Произошла ошибка при попытке получения информации из БД.
                // Надо расшифровывать дальше как тип "error".
                Error error = Error.FromJson(result);
                // Проверяем можно ли легко исправить ошибку и повторить запрос.
                if (errProcess(error, TypeMethod.USERS_GET))
                {
                    // Повторяем запрос.
                    return Find(searchName, searchMName, searchSurname);
                }
                // Не удалось исправить ошибку.
                else
                {
                    return null;
                }
            }
            // Если ключ не "error", значит можно работать дальше.
            else if (JSONHelper.IsResponse(result))
            {
                // Десериализуем из JSON.
                Response response = Response.FromJson(result);
                // Возвращаем список пользователей.

                //JsonConvert.DeserializeObject<List<RegisteredUser>>(response.data.ToString());
                List<RegisteredUser> registeredUsers = new List<RegisteredUser>();
                response.data.ForEach(el =>
                {
                    registeredUsers.Add(JsonConvert.DeserializeObject<RegisteredUser>(el.ToString()));
                });
                // Расшифровываем данные пользователя.
                registeredUsers.ForEach(u => u.DecryptDataByAES());
                // Возвращаем список пользователей.
                return registeredUsers;
            }
            return null;
        }

        public List<RegisteredUser> NextUsers(int count)
        {
            return null;
        }

        /// <summary>
        /// Получает подробную информацию о пользоателях с заданными ID.
        /// </summary>
        /// <param name="userIds">ID пользователей.</param>
        /// <returns>Информация о пользователях.</returns>
        public List<RegisteredUser> ById(string api_token, string[] userIds)
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
            string result = httpPostJSONAsync("users.get/", usersJSON, urlParam).Result;
            // Если полученный ответ это точно Json и точно объект или массив
            if (JSONHelper.IsError(result))
            {
                // Произошла ошибка при попытке получения информации из БД.
                // Надо расшифровывать дальше как тип "error".
                Error error = Error.FromJson(result);
                // Проверяем можно ли легко исправить ошибку и повторить запрос.
                if (errProcess(error, TypeMethod.USERS_GET))
                {
                    // Повторяем запрос.
                    return ById(api_token, userIds);
                }
                // Не удалось исправить ошибку.
                else
                {
                    return null;
                }
            }
            // Если ключ не "error", значит можно работать дальше.
            else if (JSONHelper.IsResponse(result))
            {
                // Десериализуем из JSON.
                Response response = Response.FromJson(result);
                // Возвращаем список пользователей.

                //JsonConvert.DeserializeObject<List<RegisteredUser>>(response.data.ToString());
                List<RegisteredUser> registeredUsers = new List<RegisteredUser>();
                response.data.ForEach(el =>
                {
                    registeredUsers.Add(JsonConvert.DeserializeObject<RegisteredUser>(el.ToString()));
                });
                // Расшифровываем данные пользователя.
                registeredUsers.ForEach(u => u.DecryptDataByAES());
                // Возвращаем список пользователей.
                return registeredUsers;
            }
            return null;
        }

        /// <summary>
        /// Получение информации о пользователе с заданным ID
        /// </summary>
        public RegisteredUser ById(string api_token, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            // Засовываем идентификатор пользователя в массив, чтобы отправить его в функцию GetUsersById
            string[] userIds = { userId };
            // Возвращаем список пользователей.
            List<RegisteredUser> registeredUsers = ById(api_token, userIds);
            // Может возвращаться, когда пользователь не найден.
            if (registeredUsers == null)
            {
                //_ = MessageBox.Show("Пользователь не найден");
                return null;
            }
            // На всякий случай. Проверяем количество пользователей.
            else if (registeredUsers.Count == 0)
            {
                Console.WriteLine("Пользователей с таким ID не найдено в БД.");
                return null;
            }
            else if (registeredUsers.Count > 1)
            {
                throw new Exception("При запросе одного конкретного пользователя, сервер вернул " +
                    registeredUsers.Count + " пользователей!");
            }
            // Возвращаем пользователя.
            return registeredUsers[0];
        }


    }
}
