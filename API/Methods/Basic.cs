using nsAPI.Entities;
using Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using nsAPI.JSON;

namespace nsAPI.Methods
{

    /// <summary>
    /// Параметры возврата количества и сдвига.
    /// </summary>
    public class SettingsFind
    {
        /// <summary>
        /// На сколько записей надо сдвинуть возврат.
        /// </summary>
        public int Shift;
        /// <summary>
        /// Сколько записей максимум надо вернуть.
        /// </summary>
        public int Count;

        public SettingsFind()
        {
            Count = 10;
            Shift = 0;
        }
    }

    abstract class Basic
    {
        public static string Nothing = "Nothing";
        public static string OK = "OK";

        // Ссылка для запросов.
        protected readonly string apiURL = "http://api.great-duet.localhost/";
       // protected readonly string apiURL = "http://api.great-duet.ru/";
        //protected readonly string apiURL = "http://10.0.2.2/api/";

        /// <summary>
        /// Вспопогмательная строка при POST-запросах.
        /// </summary>
        protected const string MIME_JSON = "application/json";

        /// Для работы с HTTP.
        protected HttpClient httpClient;

        public Response Response { get; set; }

        public Basic(string apiURL = null)
        {
            if (apiURL != null) this.apiURL = apiURL;

            // HTTP.
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                // Куки.
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            httpClient = new HttpClient(clientHandler);
            // Настройка HTTP.
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent.Chrome);
            // Чтобы не появлялась ошибка: "Unable to read data from the transport
            // connection: Operation aborted.'"
            // Источник: https://stackoverflow.com/questions/66209674/unable-to-read-data-from-the-transport-connection-operation-canceled
            httpClient.Timeout = TimeSpan.FromMinutes(10); //Eg. 10mins timeout
            //httpRequest.EnableEncodingContent = true;
        }        

        /// <summary>
        /// Отправляет POST-запрос на указанный метод с данными в формате JSON.
        /// </summary>
        /// <param name="method">Метод на сервере.</param>
        /// <param name="JSON">Данные в формате JSON.</param>
        /// <returns>Строка ответка сервера.</returns>
        protected async Task<Response> httpPostJSONAsync(string method, string sJSON, Dictionary<string, string> reqParams = null)
        {
            // Создаем объект для работы с URL и задаем ему базовый адрес: host + method.
            var mynet = new MyNet(apiURL + method);
            // Если были заданы параметры, то добавляем их в объект конструирования URL.
            if (reqParams != null) mynet.AddDict(reqParams);
            // Получаем URL.
            string url = mynet.GetUrl();
            // Создаем объект данных HTTP и задаем тип данных - JSON.
            using (var content = new StringContent(sJSON, Encoding.UTF8, MIME_JSON))
            {
                // Указываем в заголовке пакета, что данные JSON.
                content.Headers.ContentType = new MediaTypeHeaderValue(MIME_JSON);
                //
                HttpResponseMessage httpResponse = null;
                //
                try
                {
                    // Ассинхронно отправляем запрос и получаем ответ.
                    httpResponse = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                }
                catch (HttpRequestException reqexp)
                {
                    var k = (System.Net.Sockets.SocketException)(reqexp.InnerException);
                    return Response = new Response(k);
                }
                // Проверяем ответ. Если код ответа НЕ 200-299, то ошибка.
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return Response = new Response(httpResponse);
                }
                // Получаем ответ ассинхронно в виде строки.
                string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                // Вовзвращаем.
                return Response = new Response(jsonResponse);
            }
        }

        protected async Task<Response> httpGetAsync(string method, Dictionary<string,string> reqParams = null)
        {
            // Создаем объект для работы с URL и задаем ему базовый адрес: host + method.
            var mynet = new MyNet(apiURL + method);
            // Если были заданы параметры, то добавляем их в объект конструирования URL.
            if (reqParams != null) mynet.AddDict(reqParams);
            // Получаем URL.
            string url = mynet.GetUrl();
            //
            HttpResponseMessage httpResponse;
            //
            try
            {
                // Ассинхронно отправляем запрос и получаем ответ.
                httpResponse = await httpClient.GetAsync(url).ConfigureAwait(false);
                // Проверяем ответ. Если код ответа НЕ 200-299, то возвращаем пустую строку.
            }
            catch (HttpRequestException reqexp)
            {
                var k = (System.Net.Sockets.SocketException)(reqexp.InnerException);
                return Response = new Response(k);
            }
            if (!httpResponse.IsSuccessStatusCode)
            {
                return Response = new Response(httpResponse);
            }
            // Получаем ответ ассинхронно в виде строки.
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            // Вовзвращаем.
            return Response = new Response(jsonResponse);
        }
    }
}
