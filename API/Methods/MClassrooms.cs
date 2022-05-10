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
            // Конвертируем данные из нулевой ячейки массива ответа в тип RegisteredClassroom.
            var registeredClassroom = RegisteredClassroom.FromJson(httpResponse.data[0].ToString());
            // Расшифровываем токен.
            registeredClassroom.DecryptByAES();
            //
            return registeredClassroom;
        }

        /// <summary>
        /// Получает информацию о классе с заданными ID.
        /// </summary>
        /// <param name="userIds">ID классов.</param>
        /// <returns>Информация о классах.</returns>
        public async Task<List<RegisteredClassroom>> ByIdAsync(string api_token, string[] classroomIds)
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
            // Возвращаем список классов.
            List<RegisteredClassroom> registeredClassroom = new List<RegisteredClassroom>();
            //
            httpResponse.data.ForEach(el =>
            {
                registeredClassroom.Add(JsonConvert.DeserializeObject<RegisteredClassroom>(el.ToString()));
            });
            // Расшифровываем данные класса.
            registeredClassroom.ForEach(u => u.DecryptByAES());
            // Возвращаем список классов.
            return registeredClassroom;
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
                //TODO: exception...
            }
            // Засовываем идентификатор класса в массив, чтобы отправить его в функцию получения списка классов.
            string[] classroomIds = { classroomId };
            // Возвращаем список классов.
            List<RegisteredClassroom> registeredClassroom = await ByIdAsync(api_token, classroomIds);
            // Возвращаем класс.
            return registeredClassroom[0];
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
            urlParam.Add("userId", userId);
            // Добавляем роль пользователя в классах в параметры запроса.
            if (idrole == null) idrole = "0";
            urlParam.Add("idrole", idrole);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("classes.byUser/", urlParam);
            // Возвращаем список классов.
            List<RegisteredClassroom> registeredClassroom = new List<RegisteredClassroom>();
            //
            httpResponse.data.ForEach(el =>
            {
                registeredClassroom.Add(JsonConvert.DeserializeObject<RegisteredClassroom>(el.ToString()));
            });
            // Расшифровываем данные класса.
            registeredClassroom.ForEach(u => u.DecryptByAES());
            // Возвращаем список классов.
            return registeredClassroom;
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
            // Возвращаем список классов.
            List<RegisteredClassroom> registeredClassroom = new List<RegisteredClassroom>();
            //
            httpResponse.data.ForEach(el =>
            {
                registeredClassroom.Add(JsonConvert.DeserializeObject<RegisteredClassroom>(el.ToString()));
            });
            // Расшифровываем данные класса.
            registeredClassroom.ForEach(u => u.DecryptByAES());
            // Возвращаем список классов.
            return registeredClassroom;
        }

        public async Task<bool> AddStudent(string api_token, string id_user, string id_class)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // В данные POST добавляем параметры запроса.
            Dictionary<string, string> postParam = new Dictionary<string, string>();
            // Добавление по ключу "id" идентификатор пользователя.
            postParam["id_user"] = Encryption.AESHelper.EncryptString(id_user);
            // Добавление по ключу "id" идентификатор класса.
            postParam["id_class"] = Encryption.AESHelper.EncryptString(id_class);
            // 1 - Администратор, 2 - Ученик, 3 - Учитель.
            // Таблица "roles".
            string id_role = "2";
            postParam["id_role"] = Encryption.AESHelper.EncryptString(id_role);

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string postParamsJson = JsonConvert.SerializeObject(postParam);
            // Отправляем на сервер.
            var httpResponse = await httpPostJSONAsync("user.intoclass/", postParamsJson, urlParam);
            // Ответ.
            return httpResponse.data[0].ToString() == "OK";
        }
    }
}
