using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Helpers
{
    public static class UserAgent
    {
        public static string Chrome => "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 YaBrowser/20.9.3.136 Yowser/2.5 Safari/537.36";
        public static string Firefox => "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
        public static string Edge => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";

    }

    public class httpClientFab: IDisposable
    {
        public httpClientFab()
        {
            httpClients = new List<TClient>();
            httpClients.Add(new TClient { Client = getNew(), IsBusy = true });
        }
        class TClient
        {
            public HttpClient Client;
            public bool IsBusy;
        }

        private HttpClient getNew()
        {
            // HTTP.
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                // Куки.
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            var httpClient = new HttpClient(clientHandler);
            // Настройка HTTP.
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent.Chrome);
            // Чтобы не появлялась ошибка: "Unable to read data from the transport
            // connection: Operation aborted.'"
            // Источник: https://stackoverflow.com/questions/66209674/unable-to-read-data-from-the-transport-connection-operation-canceled
            httpClient.Timeout = TimeSpan.FromMinutes(10); //Eg. 10mins timeout
                                                           //httpRequest.EnableEncodingContent = true;
            return httpClient;
        }

        List<TClient> httpClients;
        public HttpClient GethttpClient()
        {
            TClient client = httpClients.SingleOrDefault(c => !c.IsBusy);
            if (client != null)
            {
                client.IsBusy = true;
                return client.Client;
            }
            else
            {
                
                httpClients.Add(new TClient { Client = getNew(), IsBusy = true });
                return httpClients.Last().Client;
            }
        }


        public void IsDOne(HttpClient client)
        {
            for (int i = 0; i < httpClients.Count; i++)
            {
                if (httpClients[i].Client == client)
                {
                    httpClients[i].IsBusy = false;
                    if (i != 0) httpClients.RemoveAt(i);
                    break;
                }
            }
        }

        public void Dispose()
        {
            httpClients.ForEach(c => c.Client.Dispose());
        }
    }

    public class MyNet
    {
        NameValueCollection valCol;
        UriBuilder uriBuilder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Полный адрес без параметров запроса.</param>
        public MyNet(string url)
        {
            uriBuilder = new UriBuilder(url);
            uriBuilder.Port = -1;
            valCol = HttpUtility.ParseQueryString(uriBuilder.Query);
        }

        public void Add(string k, string v)
        {
            valCol.Add(k, v);
        }

        // Доюавить словарь запроосов.
        public void AddDict(Dictionary<string,string> reqParams)
        {
            foreach (KeyValuePair<string,string> kvp in reqParams)
            {
                Add(kvp.Key, kvp.Value);
            }
        }

        // Получить результирующий адрес запроса с параметрами.
        public string GetUrl()
        {
            uriBuilder.Query = valCol.ToString();
            return uriBuilder.ToString();
        }
    }
}
