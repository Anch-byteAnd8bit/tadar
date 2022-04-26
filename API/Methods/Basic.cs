using nsAPI.Entities;
using nsAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace nsAPI.Methods
{
    class SettingsFind
    {
        public int Shift;
        public int Count;

        public SettingsFind()
        {
            Count = 10;
            Shift = 0;
        }
    }

    abstract class Basic
    {
        // Ссылка для запросов.
        protected readonly string apiURL = "http://api.great-duet.localhost/";


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

        /// <summary>
        /// Отправляет POST-запрос на указанный метод с данными в формате JSON.
        /// </summary>
        /// <param name="method">Метод на сервере.</param>
        /// <param name="JSON">Данные в формате JSON.</param>
        /// <returns>Строка ответка сервера.</returns>
        protected async Task<string> httpPostJSONAsync(string method, string sJSON, Dictionary<string, string> reqParams = null)
        {
            StringContent content = null;
            try
            {
                // Создаем объект для работы с URL и задаем ему базовый адрес: host + method.
                var mynet = new MyNet(apiURL + method);
                // Если были заданы параметры, то добавляем их в объект конструирования URL.
                if (reqParams != null) mynet.AddDict(reqParams);
                // Получаем URL.
                string url = mynet.GetUrl();
                // Создаем объект данных HTTP и задаем тип данных - JSON.
                content = new StringContent(sJSON, Encoding.UTF8, MIME_JSON);
                // Указываем в заголовке пакета, что данные JSON.
                content.Headers.ContentType = new MediaTypeHeaderValue(MIME_JSON);
                // Ассинхронно отправляем запрос и получаем ответ.
                HttpResponseMessage httpResponse = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                // Проверяем ответ. Если код ответа 200-299, то возвращаем пустую строку.
                if (!httpResponse.IsSuccessStatusCode)
                {
                    return string.Empty;
                }
                // Получаем ответ ассинхронно в виде строки.
                string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                // Выводим в консоль.
                Console.WriteLine(jsonResponse);
                // Вовзвращаем.
                return jsonResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                if (content != null) content.Dispose();
            }
        }

        protected async Task<string> httpGetAsync(string method, Dictionary<string,string> reqParams = null)
        {
            try
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
                    return string.Empty;
                }
                // Получаем ответ ассинхронно в виде строки.
                string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                // Выводим в консоль.
                Console.WriteLine(jsonResponse);
                // Вовзвращаем.
                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
            }
        }

        /// <summary>
        /// обработка ошибок:
        /// </summary>
        /// <returns>True - можно повторить команду и False - нельзя</returns>
        protected bool errProcess(Error error, TypeMethod typeMethod)
        {
            switch (error.errorInfo.Type)
            {
                case 100: // ERR_NotEnoughInf - не все данные, необходимые для выполнения запроса, были переданы.

                    if (typeMethod == TypeMethod.USERS_REG)
                    {
                        // Надо требовать проверить поля.
                        Console.WriteLine("Не все данные, необходимые для выполнения запроса, были переданы!");
                    }

                    break;

                case 101: // ERR_UserAlreadyReg - пользователь с такими логином и/или email'ом уже зерегистрированы
                    if (typeMethod == TypeMethod.USERS_REG)
                    {
                        // Необходимо либо поменять данные для регистрации, либо
                        // войти в аккаунт, либо восстановить доступ в аккаунт.

                        Console.WriteLine("Пользователь с такими данными уже зарегистрирован!");
                    }
                    break;

                case 102: // ERR_UserNotFound - указанный пользователь не найден в БД.
                    if (typeMethod == TypeMethod.USERS_AUTH)
                    {
                        Console.WriteLine("Указанный пользователь не найден в БД!");
                    }
                    else if (typeMethod == TypeMethod.USERS_GET)
                    {
                        Console.WriteLine("Указанный пользователь не найден в БД!");
                    }
                    break;

                case 103: // ERR_DBNotAvailable - проблема с доступом к БД.
                    Console.WriteLine("Проблема на сервере: нет подключения к БД!");
                    break;
                case 104: // ERR_SecureKeyProblem - проблема с ключом доступа.
                    Console.WriteLine("Возникла проблема с ключом доступа к серверу: " +
                        error.errorInfo.Additional);
                    break;
                case 105: // ERR_DBProblem - проблемы при работе с БД.
                    Console.WriteLine("Возникла проблема при работе с БД: " +
                        error.errorInfo.Additional);
                    break;
                //106,107
                case (long)TypeMethod.GENDERS_GET:
                    Console.WriteLine("Возникла проблема при получении списка полов от сервера: " +
                        error.errorInfo.Additional);
                    break;
                default:
                    Console.WriteLine("Unknown error. " + error.errorInfo.Additional);
                    break;
            }
            return false;
        }
    }
}
