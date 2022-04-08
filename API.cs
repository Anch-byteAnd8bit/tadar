using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using xNet;

namespace tadar
{
    class API
    {
        public const string __APPID = "APPLICATION_ID";  //ID приложения
        private const string __APIURL = "http://api.great-duet.localhost/";  //Ссылка для запросов
        private string _Token;  //Токен, использующийся при запросах
        private HttpRequest httpRequest;

        public API(string AccessToken)
        {
            _Token = AccessToken;
            httpRequest = new HttpRequest();
            httpRequest.Cookies = new CookieDictionary();
        }

        
        public Dictionary<string, string> GetInformation(string UserId, string[] Fields)
        {
            httpRequest.AddUrlParam("var", "11");
            // Получаем отыет от сервера в виде строки. В строке должен быть ответ в формате JSON.
            string Result = httpRequest.Get(__APIURL + "users.auth").ToString();
            MessageBox.Show(Result);
            Dictionary<string, string> Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
            return Dict;
        }

        /// <summary>
        /// Получение заданной информации о пользователе с заданным ID
        /// </summary>
        /// Пример JSON:
        /// {
        ///     'user':{
        ///         'id':'123',
        ///         'fname':'Ivan',
        ///         'lname':'Ivanov',
        ///         'mname':'Ivanovich'
        ///     }
        /// }
        public string GetUserById(string userId)
        {
            // Добавляем в параметры запроса ID пользователя.
            httpRequest.AddUrlParam("user_ids", userId);
            // Отправляем запрос и получаем ответ.
            string Result = httpRequest.Get(__APIURL + "users.get").ToString();
            // Десериализуем ответ.
            Dictionary<string, object> tmp = JsonConvert.DeserializeObject<Dictionary<string, object>>(Result);
            var data = (tmp["user"] as Dictionary<string, object>);
            Console.WriteLine(data["id"] + " " + data["fname"] + data["lname"] + data["mname"]);

            //TODO: выводить сразу обработанный результат или просто ответ?
            return null;//Dict["name"];
        }

        public string GetCountryById(string CountryId)  //Перевод ID страны в название
        {
            HttpRequest GetCountryById = new HttpRequest();
            GetCountryById.AddUrlParam("country_ids", CountryId);
            GetCountryById.AddUrlParam("access_token", _Token);
            GetCountryById.AddUrlParam("v", "5.52");
            string Result = GetCountryById.Get(__APIURL + "database.getCountriesById").ToString();
            Result = Result.Substring(13, Result.Length - 15);
            Dictionary<string, string> Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
            return Dict["name"];
        }
    }
}
