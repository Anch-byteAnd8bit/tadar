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
    }
}
