using Helpers;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

    abstract class Basic: IDisposable
    {
        // Ссылка для запросов.
        protected readonly string apiURL = "http://api.great-duet.localhost/";
        //protected readonly string apiURL = "http://api.great-duet.ru/";
        //protected readonly string apiURL = "http://10.0.2.2/api/";
        //protected readonly string apiURL = "http://192.168.0.104/api/";

        /// <summary>
        /// Вспопогмательная строка при POST-запросах.
        /// </summary>
        protected const string MIME_JSON = "application/json";
        /// <summary>
        /// Вспопогмательная строка, указывающая на тип полученных данных - JPEG.
        /// </summary>
        protected const string MIME_JPEG = "image/jpeg";
        /// Для работы с HTTP.
        //protected HttpClient httpClient;

        //

        private static httpClientFab httpClients;

        public Response Response { get; set; }

        public Basic(string apiURL = null)
        {
            if (apiURL != null) this.apiURL = apiURL;

            /*// HTTP.
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
            //httpRequest.EnableEncodingContent = true;*/
            httpClients = new httpClientFab();
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
                var httpClient = httpClients.GethttpClient();
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
                finally
                {
                    httpClients.IsDOne(httpClient);
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

            var httpClient = httpClients.GethttpClient();
            try
            {
                // Ассинхронно отправляем запрос и получаем ответ.
                httpResponse = await httpClient.GetAsync(url).ConfigureAwait(false);
            }
            catch (HttpRequestException reqexp)
            {
                var k = (System.Net.Sockets.SocketException)(reqexp.InnerException);
                return Response = new Response(k);
            }
            finally
            {
                httpClients.IsDOne(httpClient);
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

        protected async Task<Response> httpGetStreamAsync(string method, Dictionary<string, string> reqParams = null)
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

            var httpClient = httpClients.GethttpClient();
            try
            {
                // Ассинхронно отправляем запрос и получаем ответ.
                httpResponse = await httpClient.GetAsync(url).ConfigureAwait(false);
            }
            catch (HttpRequestException reqexp)
            {
                var k = (System.Net.Sockets.SocketException)(reqexp.InnerException);
                return Response = new Response(k);
            }
            finally
            {
                httpClients.IsDOne(httpClient);
            }
            if (!httpResponse.IsSuccessStatusCode)
            {
                return Response = new Response(httpResponse);
            }
            // Получаем ответ ассинхронно в виде строки.
            string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var mt = httpResponse.Content.Headers.ContentType.MediaType;
            if (mt == MIME_JSON)
            {
                return Response = new Response(jsonResponse);
            }
            else if (mt == MIME_JPEG)
            {
                // Вовзвращаем поток.
                return Response = new Response(await httpResponse.Content.ReadAsStreamAsync());
            }
            else
            {
                return Response = new Response(TError.UnknownError, "Получен ответ неизвестного типа!");
            }
        }

        ///// <summary>
        ///// Получает ответ от сервера в виде потока. [Т.е. нет проверки на то, а точно ли это файл].
        ///// </summary>
        ///// <param name="method"></param>
        ///// <param name="reqParams"></param>
        ///// <returns></returns>
        //protected async Task<Response> httpGetStreamAsync(string method, Dictionary<string, string> reqParams = null)
        //{
        //    // Создаем объект для работы с URL и задаем ему базовый адрес: host + method.
        //    var mynet = new MyNet(apiURL + method);
        //    // Если были заданы параметры, то добавляем их в объект конструирования URL.
        //    if (reqParams != null) mynet.AddDict(reqParams);
        //    // Получаем URL.
        //    string url = mynet.GetUrl();
        //    //
        //    System.IO.Stream httpResponseStream;
        //    //

        //    var httpClient = httpClients.GethttpClient();
        //    try
        //    {
        //        // Ассинхронно отправляем запрос и получаем ответ.
        //        httpResponseStream = await httpClient.GetStreamAsync(url).ConfigureAwait(false);
        //    }
        //    catch (HttpRequestException reqexp)
        //    {
        //        var k = (System.Net.Sockets.SocketException)(reqexp.InnerException);
        //        return Response = new Response(k);
        //    }
        //    finally
        //    {
        //        httpClients.IsDOne(httpClient);
        //    }
        //    // Вовзвращаем.
        //    return Response = new Response(httpResponseStream);
        //}

        public void Dispose()
        {
            httpClients.Dispose();
        }
    }
}
