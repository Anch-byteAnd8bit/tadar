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
        public bool TryReg(string api_token, ClassroomForReg classroom, out RegisteredClassroom registeredClassroom)
        {
            // Тут Будет храниться результат запроса.
            IdentifierClassroom id;

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            //_ = httpRequest.AddUrlParam("secure_key", api_token);

            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем пользователя с зашифрованными данными.
            classroom.EncryptByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string classroomJson = classroom.ToJson();
            Console.WriteLine(classroomJson);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            string result = httpPostJSONAsync("classes.add/", classroomJson, urlParams).Result;
            // Проверяем не оказался ли результат пустым.
            if (string.IsNullOrEmpty(result))
            {
                registeredClassroom = null;
                return false;
            }
            // Если вернулась ошибка.
            if (JSONHelper.IsError(result))
            {
                // Произошла ошибка при попытке получения информации из БД.
                Error error = Error.FromJson(result);
                //
                registeredClassroom = null;
                // Обработка ошибки.
                return errProcess(error, TypeMethod.USERS_REG);
            }
            else if (JSONHelper.IsResponse(result))
            {
                // Конвертируем строку JSON в тип response.
                Response response = Response.FromJson(result);
                // Конвертируем данные из нулевой ячейки массива ответа в тип IdentifierClassroom.
                id = IdentifierClassroom.FromJson(response.data[0].ToString());
                // Расшифровываем токен.
                id.DecryptByAES();
                //
                registeredClassroom = new RegisteredClassroom
                {
                    Name = classroom.Name,
                    Description = classroom.Description,
                    ID = id.ID,
                    id_Journal = id.id_Journal
                };
                // Успех!
                return true;
            }
            // Если полученная строка незнакома.
            else
            {
                Console.WriteLine(result);
                Console.WriteLine("Полученное значение неизвестно." +
                    "\nЗначение скопировано в буфер обмена!");
                registeredClassroom = null;
                return false;
            }
            //return false;
        }
    }
}
