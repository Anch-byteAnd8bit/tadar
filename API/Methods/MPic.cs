using Helpers;
using Newtonsoft.Json;
using nsAPI.Entities;
using nsAPI.JSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace nsAPI.Methods
{
    class MPic : Basic
    {
        /// <summary>
        /// Возвращает изображение в формате потока Stream.
        /// </summary>
        /// <param name="pic">Изображение типа Pic.</param>
        /// <returns>Изображение в потоке Stream.</returns>
        public async Task<Stream> GetImage(string api_token, Pic pic)
        {
            // Добавляем в запрос тип справочника.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["ID"] = pic.ID ?? "0";
            urlParams["Alias"] = pic.Alias ?? "0";
            urlParams["Path"] = "0";
            urlParams["Size"] = "0";
            // Получаем ответ от сервера.
            var httpResponse = await httpGetStreamAsync("image.get", urlParams);
            if (httpResponse.Data != null)
            {
                return (httpResponse.Data[0] as Stream);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает изображение в формате потока Stream.
        /// </summary>
        /// <param name="ID">ID Изображения.</param>
        /// <returns>Изображение в потоке Stream.</returns>
        public async Task<Stream> GetImageByID(string api_token, string ID)
        {
            var p = new Pic
            {
                Alias = null,
                ID = ID,
                Path = null,
                Size = 0
            };
            return await GetImage(api_token, p);
        }

        /// <summary>
        /// Возвращает изображение в формате потока Stream.
        /// </summary>
        /// <param name="Alias">Alias(имя/псевдоним) изображения.</param>
        /// <returns>Изображение в потоке Stream.</returns>
        public async Task<Stream> GetImageByAlias(string api_token, string Alias)
        {
            var p = new Pic
            {
                Alias = Alias,
                ID = null,
                Path = null,
                Size = 0
            };
            return await GetImage(api_token, p);
        }
    }
}
