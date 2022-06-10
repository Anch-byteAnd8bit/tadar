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
    class MClassrooms: Basic
    {

        /// <summary>
        /// Регистрация класса.
        /// </summary>
        /// <param name="classroom">Данные регистрации класса.</param>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <returns>Возвращает объект класса RegisteredClassroom - зарегистрированный класс.</returns>
        public async Task<RegisteredClassroom> RegAsync(string api_token, ClassroomForReg classroom)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем пользователя с зашифрованными данными.
            classroom.EncryptByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string classroomJson = classroom.ToJson();
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("classes.add/", classroomJson, urlParams);
            if (httpResponse.Data != null)
            {
                // Конвертируем данные из нулевой ячейки массива ответа в тип RegisteredClassroom.
                var registeredClassroom = RegisteredClassroom.FromJson(httpResponse.Data[0].ToString());
                // Расшифровываем токен.
                registeredClassroom.DecryptByAES();
                //
                return registeredClassroom;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получает информацию о классе с заданными ID.
        /// </summary>
        /// <param name="userIds">ID классов.</param>
        /// <returns>Информация о классах.</returns>
        public async Task<List<RegisteredClassroom>> ByIDsAsync(string api_token, string[] classroomIds)
        {
            if (classroomIds == null || classroomIds.Count() <= 0)
            {
                return null;
            }
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // Добавление в массив по ключу "ids", список идентификаторов классов.
            d.Add("ids", classroomIds);
            // Сериализация (конвертирование в формат JSON).
            string usersJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("class.get/", usersJSON, urlParam);
            if (httpResponse.Data != null)
            {
                // Возвращаем список классов.
                List<RegisteredClassroom> registeredClassroom = new List<RegisteredClassroom>();
                //
                httpResponse.Data.ForEach(el =>
                {
                    registeredClassroom.Add(JsonConvert.DeserializeObject<RegisteredClassroom>(el.ToString()));
                });
                // Расшифровываем данные класса.
                registeredClassroom.ForEach(u => u.DecryptByAES());
                // Возвращаем список классов.
                return registeredClassroom;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получает информацию о классах с заданными ID.
        /// </summary>
        /// <param name="userIds">ID классов.</param>
        /// <returns>Информация о классах.</returns>
        public async Task<RegisteredClassroom> ByIdAsync(string api_token, string classroomId)
        {
            if (string.IsNullOrWhiteSpace(classroomId))
            {
                return null;
                throw new Exception("не заданы идентификатор класса!");
            }
            // Засовываем идентификатор класса в массив, чтобы отправить его в функцию получения списка классов.
            string[] classroomIds = { classroomId };
            // Возвращаем список классов.
            List<RegisteredClassroom> registeredClassroom = await ByIDsAsync(api_token, classroomIds);
            // Возвращаем класс.
            return registeredClassroom?[0];
        }


        /// <summary>
        /// Получает основную информацию о классах пользователя с заданным ID.
        /// </summary>
        /// <param name="api_token">.</param>
        /// <param name="userId">ID пользователя.</param>
        /// <param name="idrole">идентификатор роли юзера в этом классе.</param>
        /// <returns>Информация о классах.</returns>
        public async Task<List<RegisteredClassroom>> ByUserIdAsync(string api_token, string userId, string idrole = null)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // Добавление в массив по ключу "id", идентификатора пользователя.
            urlParam.Add("id_User", userId);
            // Добавляем роль пользователя в классах в параметры запроса.
            if (idrole == null) idrole = "0"; // ВАЖНО: на сервере 0 воспринимается как
                                              // указание что роль не надо учитывать вообще!
            urlParam.Add("id_Role", idrole);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("classes.byUser/", urlParam);
            if (httpResponse.Data != null)
            {
                // Возвращаем список классов.
                List<RegisteredClassroom> registeredClassroom = new List<RegisteredClassroom>();
                //
                httpResponse.Data.ForEach(el =>
                {
                    registeredClassroom.Add(JsonConvert.DeserializeObject<RegisteredClassroom>(el.ToString()));
                });
                // Расшифровываем данные класса.
                registeredClassroom.ForEach(u => u.DecryptByAES());
                // Возвращаем список классов.
                return registeredClassroom;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<RegisteredClassroom>> GetAllAsync(string api_token, SettingsFind settings)
        {
            if (settings == null) settings = new SettingsFind();
            if (settings.Count <= 0 || settings.Shift < 0)
            {
                return null;
            }
            // Если задано слишком большое кол-во.
            settings.Count = Math.Min(settings.Count, 50);

            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            urlParam["secure_key"] = api_token;
            // Кол-во пользователей.
            urlParam["count"] = settings.Count.ToString();
            // Смещение относительно первого пользователя.
            urlParam["shift"] = settings.Shift.ToString();

            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("class.getAll", urlParam);
            if (httpResponse.Data != null)
            {
                // Возвращаем список классов.
                List<RegisteredClassroom> registeredClassroom = new List<RegisteredClassroom>();
                //
                httpResponse.Data.ForEach(el =>
                {
                    registeredClassroom.Add(JsonConvert.DeserializeObject<RegisteredClassroom>(el.ToString()));
                });
                // Расшифровываем данные класса.
                registeredClassroom.ForEach(u => u.DecryptByAES());
                // Возвращаем список классов.
                return registeredClassroom;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> AddStudent(string api_token, string id_user, string id_class)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // В данные POST добавляем параметры запроса.
            Dictionary<string, string> postParam = new Dictionary<string, string>();
            // Добавление по ключу "id" идентификатор пользователя.
            postParam["id_User"] = Encryption.AESHelper.EncryptStringB64(id_user);
            // Добавление по ключу "id" идентификатор класса.
            postParam["id_Class"] = Encryption.AESHelper.EncryptStringB64(id_class);
            // 1 - Администратор, 2 - Ученик, 3 - Учитель.
            // Таблица "roles".
            string id_role = "2";
            postParam["id_Role"] = Encryption.AESHelper.EncryptStringB64(id_role);

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string postParamsJson = JsonConvert.SerializeObject(postParam);
            // Отправляем на сервер.
            var httpResponse = await httpPostJSONAsync("student.add/", postParamsJson, urlParam);
            if (httpResponse.Data != null) {
                // Ответ.
                return httpResponse.Data[0].ToString() == "OK";
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Удаляем пользователя из класса, а также всего ответы на задания в классе!
        /// </summary>
        /// <param name="api_token">Ключ доступа для выполнения.</param>
        /// <param name="id_user">Идентификатор пользователя, которого надо исключить из класса.</param>
        /// <param name="id_class">Идентификатор класса, из которого надо исключить пользователя.</param>
        /// <returns>True - при успешном выполнении операции.</returns>
        public async Task<bool> DelStudent(string api_token, string id_User, string id_Class)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // В данные POST добавляем параметры запроса.
            Dictionary<string, string> postParam = new Dictionary<string, string>();
            // Добавление по ключу "id" идентификатор пользователя.
            postParam["id_User"] = Encryption.AESHelper.EncryptStringB64(id_User);
            // Добавление по ключу "id" идентификатор класса.
            postParam["id_Class"] = Encryption.AESHelper.EncryptStringB64(id_Class);

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string postParamsJson = JsonConvert.SerializeObject(postParam);
            // Отправляем на сервер.
            var httpResponse = await httpPostJSONAsync("student.exclude/", postParamsJson, urlParam);
            if (httpResponse.Data != null)
            {
                // Ответ.
                return httpResponse.Data[0].ToString() == "OK";
            }
            else
            {
                return false;
            }

        }
    }
}
