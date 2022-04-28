using Helpers;
using nsAPI.Entities;
using nsAPI.JSON;
using System;
using System.Collections.Generic;
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
    }
}
