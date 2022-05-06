﻿using Helpers;
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
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="user">Данные регистрации.</param>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <returns>True - при успешной регистрации.</returns>
        public async Task<RegisteredClassroom> RegAsync(string api_token, ClassroomForReg classroom)
        {
            // Тут Будет храниться результат запроса.
            IdentifierClassroom id;

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем пользователя с зашифрованными данными.
            classroom.EncryptByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string classroomJson = classroom.ToJson();
            Console.WriteLine(classroomJson);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("classes.add/", classroomJson, urlParams);
            // Конвертируем данные из нулевой ячейки массива ответа в тип IdentifierClassroom.
            id = IdentifierClassroom.FromJson(httpResponse.data[0].ToString());
            // Расшифровываем токен.
            id.DecryptByAES();
            //
            return new RegisteredClassroom
            {
                Name = classroom.Name,
                Description = classroom.Description,
                ID = id.ID,
                id_Journal = id.id_Journal
            };
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
    }
}
