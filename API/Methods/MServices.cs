using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tadar.Helpers;

namespace nsAPI.Methods
{
    internal class MServices: Basic
    {
        public async Task<NewVersion> Check(string AppVersion, string id_User)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("id_User", id_User);
            urlParam.Add("AppVersion", AppVersion);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("checkupd/", urlParam);
            if (httpResponse.Data?[0] != null)
            {
                //
                NewVersion newVersion = JsonConvert.DeserializeObject<NewVersion>(httpResponse.Data[0].ToString());
                // Возвращаем.
                return newVersion;
            }
            else
            {
                return null;
            }
        }
    }
}
