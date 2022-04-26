using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace nsAPI.Helpers
{
    public static class UserAgent
    {
        public static string Chrome => "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.102 YaBrowser/20.9.3.136 Yowser/2.5 Safari/537.36";
        public static string Firefox => "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";
        public static string Edge => "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362";

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
