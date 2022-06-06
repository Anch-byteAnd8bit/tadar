﻿using Helpers;
using Newtonsoft.Json;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace nsAPI.Methods
{
    class MTheories : Basic
    {

        public async Task<Theory> RegAsync(string api_token, Theory theory)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем теорию с зашифрованными данными.
            Theory theoryToSend = theory.Clone();
            theoryToSend.Encrypt();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string theoryJson = theoryToSend.ToJson();
            //Clipboard.SetText(theoryJson);
            //Console.WriteLine(theoryJson);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("theory.add/", theoryJson, urlParams);
            if (httpResponse.Data != null)
            {
                // Конвертируем данные из нулевой ячейки массива ответа в тип IdentifierClassroom.
                Identifier id = Identifier.FromJson(httpResponse.Data[0].ToString());
                // Расшифровываем.
                //ids.DecryptByAES();
                //
                theory.ID = id.ID;
                return theory;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получает информацию о  с заданными ID.
        /// </summary>
        /// <param name="api_token">Токен ядл доступа к АПИ.</param>
        /// <param name="theoriesIDs">ID теорий.</param>
        /// <returns>Теоретическая информация.</returns>
        public async Task<List<Theory>> ByIDsAsync(string api_token, List<string> theoriesIDs)
        {
            if (theoriesIDs == null || theoriesIDs.Count() <= 0)
            {
                return null;
            }
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // Добавление в массив по ключу "ids", список идентификаторов объектов.
            d.Add("ids", theoriesIDs);
            // Сериализация (конвертирование в формат JSON).
            string usersJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("theory.get/", usersJSON, urlParam);
            if (httpResponse.Data != null)
            {
                // Возвращаем список классов.
                List<Theory> registeredTheories = new List<Theory>();
                //
                httpResponse.Data.ForEach(el =>
                    registeredTheories.Add(Theory.FromJson(el.ToString())));
                // Расшифровываем данные класса.
                registeredTheories.ForEach(u => { if (u != null) u.Decrypt(); });
                // Возвращаем список классов.
                return registeredTheories;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Получает информацию о классах с заданными ID.
        /// </summary>
        /// <param name="userIds">ID классов.</param>
        /// <returns>Информация о классах.</returns>
        public async Task<Theory> ByIdAsync(string api_token, string theoryId)
        {
            if (string.IsNullOrWhiteSpace(theoryId))
            {
                return null;
                //TODO: exception...
            }
            // Засовываем идентификатор теории в массив, чтобы отправить его в функцию получения списка теории.
            List<string> theoryIdIds = new List<string>{ theoryId };
            // Возвращаем список теории.
            List<Theory> registeredTheories = await ByIDsAsync(api_token, theoryIdIds);
            // Возвращаем теорию.
            return registeredTheories?[0];
        }

        /// <summary>
        /// Возаращет список теории по заданному ID класса.
        /// </summary>
        /// <param name="api_token"></param>
        /// <param name="id_class"></param>
        /// <returns></returns>
        public async Task<List<Theory>> ByClassIdAsync(string api_token, string id_class)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // Добавление в массив по ключу "id", идентификатора класса.
            urlParam.Add("id_Class", id_class);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("theory.get/", urlParam);
            if (httpResponse.Data != null)
            {


                // Возвращаем список классов.
                List<Theory> registeredTheories = new List<Theory>();
                //
                httpResponse.Data.ForEach(el =>
                {
                    registeredTheories.Add(JsonConvert.DeserializeObject<Theory>(el.ToString()));
                });
                // Расшифровываем данные класса.
                registeredTheories.ForEach(u => u.Decrypt());
                // Возвращаем список классов.
                return registeredTheories;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Удаление теории по их идентификаторам.
        /// </summary>
        /// <param name="api_token">Ключ доступа к АПИ.</param>
        /// <param name="IDs">Массив идентификаторов.</param>
        /// <returns>True - если операция выполнена успешно.</returns>
        public async Task<bool> DelTheories(string api_token, string[] IDs)
        {
            if (IDs == null || IDs.Count() <= 0)
            {
                return false;
            }
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // Добавление в массив по ключу "ids", список идентификаторов объектов.
            d.Add("ids", IDs);
            // Сериализация (конвертирование в формат JSON).
            string usersJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("theory.del/", usersJSON, urlParam);
            // Ответ.
            return httpResponse.Data?[0]?.ToString() == "OK";
        }

        public async Task<bool> UpdAsync(string api_token, Theory theory)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем теорию с зашифрованными данными.
            Theory theoryToSend = theory.Clone();
            theoryToSend.Encrypt();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string theoryJson = theoryToSend.ToJson();
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("theory.upd/", theoryJson, urlParams);
            //
            return httpResponse.Data?[0].ToString() == "OK";
        }
    }
}

