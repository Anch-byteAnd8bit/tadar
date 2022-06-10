using Helpers;
using Newtonsoft.Json;
using nsAPI.Entities;
using nsAPI.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nsAPI.Methods
{
    /// <summary>
    /// Какой(какие) словарь(словари) надо вернуть.
    /// </summary>
    enum FilterDict
    {
        /// <summary>
        /// Вернуть только словарь пользователя.
        /// </summary>
        onlyUser = 1,
        /// <summary>
        /// Вернуть только общий словарь.
        /// </summary>
        onlyCommon = 2,
        /// <summary>
        /// Вернуть и словарь пользовтаеля и общий в одном списке.
        /// </summary>
        combined = 3
    }

    class MDict : Basic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api_token"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        public async Task<bool> AddWordsAsync(string api_token, List<Word> words)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем слова с зашифрованными данными.
            words.ForEach(word => word.Encrypte());
            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string wordsJson = JsonConvert.SerializeObject(words);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("dict.add/", wordsJson, urlParams);
            if (httpResponse.Data != null)
            {
                // 
                return httpResponse.Data[0].ToString() == Response.OK;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="api_token"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<bool> AddWordAsync(string api_token, Word word) =>
            await AddWordsAsync(api_token, new List<Word>() { word });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="api_token"></param>
        /// <param name="id_User"></param>
        /// <param name="settingsFind"></param>
        /// <returns></returns>
        public async Task<List<Word>> GetUserAsync(string api_token, string id_User, SettingsFind settingsFind)
        {
            if (settingsFind == null) settingsFind = new SettingsFind();

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            urlParam.Add("shift", settingsFind.Shift.ToString());
            urlParam.Add("count", settingsFind.Count.ToString());
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // Только словарь пользовтаеля.
            d.Add("filter", ((int)FilterDict.onlyUser).ToString());
            d.Add("id_User", id_User);
            // Сериализация (конвертирование в формат JSON).
            string postDataJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("dict.get/", postDataJSON, urlParam);
            if (httpResponse.Data != null)
            {
                // Возвращаем список классов.
                List<Word> dict = new List<Word>();
                //
                httpResponse.Data.ForEach(el =>
                {
                    dict.Add(JsonConvert.DeserializeObject<Word>(el.ToString()));
                });
                // Расшифровываем данные словаря.
                dict.ForEach(u => u.Decrypte());
                // Возвращаем список слов.
                return dict;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Word>> GetCommonAsync(string api_token, SettingsFind settingsFind)
        {
            if (settingsFind == null) settingsFind = new SettingsFind();

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            urlParam.Add("shift", settingsFind.Shift.ToString());
            urlParam.Add("count", settingsFind.Count.ToString());
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // Только общий словарь.
            d.Add("filter", ((int)FilterDict.onlyCommon).ToString());
            // Сериализация (конвертирование в формат JSON).
            string postDataJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("dict.get/", postDataJSON, urlParam);
            if (httpResponse.Data != null)
            {
                // Возвращаем список классов.
                List<Word> dict = new List<Word>();
                //
                httpResponse.Data.ForEach(el =>
                {
                    dict.Add(JsonConvert.DeserializeObject<Word>(el.ToString()));
                });
                // Расшифровываем данные словаря.
                dict.ForEach(u => u.Decrypte());
                // Возвращаем список слов.
                return dict;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Word>> GetCombinedAsync(string api_token, string id_User, SettingsFind settingsFind)
        {
            if (settingsFind == null) settingsFind = new SettingsFind();

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            urlParam.Add("shift", settingsFind.Shift.ToString());
            urlParam.Add("count", settingsFind.Count.ToString());
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // И словарь пользователя и общий.
            d.Add("filter", ((int)FilterDict.combined).ToString());
            d.Add("id_User", id_User);
            // Сериализация (конвертирование в формат JSON).
            string postDataJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("dict.get/", postDataJSON, urlParam);
            if (httpResponse.Data != null)
            {
                // Возвращаем список .
                List<Word> dict = new List<Word>();
                //
                httpResponse.Data.ForEach(el =>
                {
                    dict.Add(JsonConvert.DeserializeObject<Word>(el.ToString()));
                });
                // Расшифровываем данные словаря.
                dict.ForEach(u => u.Decrypte());
                // Возвращаем список слов.
                return dict;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Загружает аудио звучания хакасского слова, по заданному слову словаря.
        /// </summary>
        /// <param name="api_token">Ключ</param>
        /// <param name="ID">Идентификатор слова</param>
        /// <returns>Слово типа Word, в свойстве "PathAudio" которого, указан путь к аудиофайлу.</returns>
        public async Task<Word> GetAudioByIDAsync(string api_token, string ID)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            urlParam.Add("ID", ID);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("dictaud.get/", urlParam);
            if (httpResponse.Data != null)
            {
                //
                Word w = JsonConvert.DeserializeObject<Word>(httpResponse.Data[0].ToString());
                
                // Расшифровываем данные словаря.
                w.Decrypte();
                // Возвращаем.
                return w;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Загружает аудио звучания хакасского слова, по заданному слову словаря.
        /// </summary>
        /// <param name="api_token">Ключ доступа.</param>
        /// <param name="word">Слова словаря.</param>
        /// <returns>Слово типа Word, в свойстве "PathAudio" которого, указан путь к аудиофайлу.</returns>
        public async Task<Word> GetAudioByWordAsync(string api_token, Word word) => 
            await GetAudioByIDAsync(api_token, word.ID);

    }
}
