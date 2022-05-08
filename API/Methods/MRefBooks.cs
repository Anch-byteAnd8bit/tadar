using Helpers;
using Newtonsoft.Json;
using nsAPI.Entities;
using nsAPI.JSON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nsAPI.Methods
{
    class MRefBooks : Basic
    {
        public async Task<List<Refbook>> GetListGendersAsync()
        {
            // Добавляем в запрос тип справочника.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["refbook"] = "genders";

            //Dictionary<string, string> p = new Dictionary<string, string>();
            //p["refbook"] = "genders";
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync( "refbooks.get", urlParams);
            // Конвертируем данные из массива ответа в список типа Gender.
            List<Refbook> res = new List<Refbook>();
            httpResponse.data.ForEach(x =>
            {
                res.Add(JsonConvert.DeserializeObject<Refbook>(x.ToString()));
            });
            return res;
        }

        /// <summary>
        /// Добавление письменной работы в журнал.
        /// </summary>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <param name="refbook">Наименование справочника.</param>
        /// <param name="names">Данные для справочника.</param>
        public async Task<bool> AddDataToRefbook(string api_token, string refbook, List<string> names)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;
            urlParams["refbook"] = refbook;
            // Шифруем всё!
            names.ForEach(name => name = Encryption.AESHelper.EncryptString(name));
            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string refbookJson = JsonConvert.SerializeObject(names);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("refbook.add/", refbookJson, urlParams);
            // Ответ.
            return httpResponse.data[0].ToString() == "OK";
        }
    }
}
