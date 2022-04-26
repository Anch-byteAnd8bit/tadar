using Newtonsoft.Json;
using nsAPI.Entities;
using nsAPI.JSON;
using System;
using System.Collections.Generic;
using System.Text;

namespace nsAPI.Methods
{
    class MRefBooks : Basic
    {
        public List<Gender> GetListGenders()
        {
            // Добавляем в запрос тип справочника.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["refbook"] = "genders";

            //Dictionary<string, string> p = new Dictionary<string, string>();
            //p["refbook"] = "genders";
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = httpGetAsync( "refbooks.get", urlParams);
            // Проверяем не оказался ли результат пустым.
            string result = string.Empty;
            if (string.IsNullOrEmpty(result = httpResponse.Result))
            {
                return null;
            }
            // Если вернулась ошибка.
            if (JSONHelper.IsError(result))
            {
                // Произошла ошибка при попытке получения информации из БД.
                Error error = Error.FromJson(result);
                // Обработка ошибки.
                if (errProcess(error, TypeMethod.GENDERS_GET))
                {
                    // Потенциально бесконечная рекурсия!!!
                    return GetListGenders();
                }
                else
                {
                    return null;
                }
            }
            else if (JSONHelper.IsResponse(result))
            {
                // Конвертируем строку JSON в тип response.
                Response response = Response.FromJson(result);
                // Конвертируем данные из массива ответа в список типа Gender.
                List<Gender> res = new List<Gender>();
                try
                {
                    response.data.ForEach(x =>
                    {
                        res.Add(JsonConvert.DeserializeObject<Gender>(x.ToString()));
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return res;
            }
            // Если полученная строка незнакома.
            else
            {
                Console.WriteLine(result);
                Console.WriteLine("Полученное значение неизвестно." +
                    "\nЗначение скопировано в буфер обмена!");
                return null;
            }
            //return false;
        }
    }
}
