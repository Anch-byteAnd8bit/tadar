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

        // Ссылка для запросов.
        protected readonly string apiURL = "http://api.great-duet.localhost/";
        //protected readonly string apiURL = "http://api.great-duet.ru/";

        /// <summary>
        /// Вспопогмательная строка при POST-запросах.
        /// </summary>
        protected const string MIME_JSON = "application/json";

        /// Для работы с HTTP.
        protected HttpClient httpClient;

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

            //httpRequest.EnableEncodingContent = true;
        }

        protected Response ProcessingHttpResponse(string httpResponse)
        {
            // Если вернулась ошибка.
            if (JSONHelper.IsError(httpResponse))
            {
                // Произошла ошибка при попытке получения информации из БД.
                Error error = Error.FromJson(httpResponse);
                // Обработка ошибки.
                throw new ErrorResponseException(error);
            }
            else if (JSONHelper.IsResponse(httpResponse))
            {
                // Конвертируем строку JSON в тип response.
                return Response.FromJson(httpResponse);
            }
            // Если полученная строка незнакома.
            else
            {
                throw new UnknownHttpResponseException(httpResponse);
            }
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
                // Ассинхронно отправляем запрос и получаем ответ.
                var httpResponse = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                // Проверяем ответ. Если код ответа НЕ 200-299, то ошибка.
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new HttpResponseException(httpResponse);
                }
                // Получаем ответ ассинхронно в виде строки.
                string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                // Пришёл пустой ответ от сервера.
                if (string.IsNullOrEmpty(jsonResponse))
                {
                    throw new EmptyHttpResponseException();
                }
                // Вовзвращаем.
                return ProcessingHttpResponse(jsonResponse);
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
            // Ассинхронно отправляем запрос и получаем ответ.
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url).ConfigureAwait(false);
            // Проверяем ответ. Если код ответа НЕ 200-299, то возвращаем пустую строку.
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new HttpResponseException(httpResponse);
            }
            // Получаем ответ ассинхронно в виде строки.
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            // Пришёл пустой ответ от сервера.
            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new EmptyHttpResponseException();
            }
            // Выводим в консоль.
            Log.Write(jsonResponse);
            // Вовзвращаем.
            return ProcessingHttpResponse(jsonResponse);
        }
    }
}
