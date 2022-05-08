using Helpers;
using Newtonsoft.Json;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nsAPI.Methods
{
    class MWorks: Basic
    {

        /// <summary>
        /// Добавление тестовой работы в журнал.
        /// </summary>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <param name="work">Данные тестовой работы.</param>
        /// <returns>Идентификатор тестовой работы типа string.</returns>
        public async Task<string> TestWorkAddAsync(string api_token, TestWorkForAdd work)
        {
            // Тут Будет храниться результат запроса.
            Identifier id;

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем пользователя с зашифрованными данными.
            work.EncryptByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string workJson = work.ToJson();
            Console.WriteLine(workJson);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("works.add/", workJson, urlParams);
            // Конвертируем данные из нулевой ячейки массива ответа в тип IdentifierClassroom.
            id = Identifier.FromJson(httpResponse.data[0].ToString());
            // Расшифровываем.
            id.DecryptByAES();
            //
            return id.ID;
        }

        /// <summary>
        /// Добавление письменной работы в журнал.
        /// </summary>
        /// <param name="accessToken">Расшифрованные данные для доступа к API</param>
        /// <param name="work">Данные письменной работы.</param>
        /// <returns>Идентификатор письменной работы типа string.</returns>
        public async Task<string> TextWorkAddAsync(string api_token, TextWorkForAdd work)
        {
            // Тут Будет храниться результат запроса.
            Identifier id;

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем пользователя с зашифрованными данными.
            work.EncryptByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string workJson = work.ToJson();
            Console.WriteLine(workJson);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("works.add/", workJson, urlParams);
            // Конвертируем данные из нулевой ячейки массива ответа в тип IdentifierClassroom.
            id = Identifier.FromJson(httpResponse.data[0].ToString());
            // Расшифровываем.
            id.DecryptByAES();
            //
            return id.ID;
        }

        /// <summary>
        /// Получает информацию о работах с заданными ID журналов.
        /// </summary>
        /// <param name="api_token">Ключ для работы с АПИ.</param>
        /// <param name="journalIDs">ID журналов.</param>
        /// <param name="onlyHeaders">Возвращать только заголовки работ.</param>
        /// <returns>Информация о работах.</returns>
        public async Task<Works> ByJournalsIdsAsync(string api_token, string[] journalIDs, bool onlyHeaders = true)
        {
            if (journalIDs == null || journalIDs.Count() <= 0)
            {
                return null;
            }
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            urlParam.Add("onlyHeaders", onlyHeaders ? "1" : "0");
            // Создание ассоциативного массива.
            var d = new Dictionary<string, object>();
            // Добавление в массив по ключу "ids", список идентификаторов классов.
            d.Add("ids", journalIDs);
            // Сериализация (конвертирование в формат JSON).
            string dataJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("works.get/", dataJSON, urlParam);
            // Список работ.
            Works works = new Works();
            //
            httpResponse.data.ForEach(el =>
            {
                if (el.ContainsKey("WorkHeader"))
                {
                    // Получение заголовка.
                    WorkHeader workHeader = JsonConvert.DeserializeObject<WorkHeader>(el["WorkHeader"].ToString());
                    // Расшифровка заголовка.
                    workHeader.DecryptByAES();
                    // Test
                    if (workHeader.IdTypeWork == "1")
                    {
                        //
                        TestWork testWork = new TestWork();
                        //
                        testWork.WorkHeader = workHeader;
                        if (el.ContainsKey("WorkBody"))
                        {
                            testWork.WorkBody = JsonConvert.DeserializeObject<List<TestTask>>(el["WorkBody"].ToString());
                            testWork.DecryptBodyByAES();
                        }
                        // Сохраняем работу.
                        works.TestWorks.Add(testWork);
                    }// Text
                    else if (workHeader.IdTypeWork == "2")
                    {
                        //
                        TextWork textWork = new TextWork();
                        textWork.WorkHeader = workHeader;
                        if (el.ContainsKey("WorkBody"))
                        {
                            textWork.WorkBody = JsonConvert.DeserializeObject<List<TextTask>>(el["WorkBody"].ToString());
                            textWork.DecryptBodyByAES();
                        }
                        // Сохраняем работу.
                        works.TextWorks.Add(textWork);
                    }
                }
            });
            // Возвращаем список классов.
            return works;
        }

        /// <summary>
        /// Получает информацию о работах с заданным ID журнала.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="journalId">ID журналов.</param>
        /// <param name="onlyHeaders">Возвращать только заголовки работ.</param>
        /// <returns>Информация о классах.</returns>
        public async Task<Works> ByJournalIdAsync(string api_token, string journalId, bool onlyHeaders = true)
        {
            if (string.IsNullOrWhiteSpace(journalId))
            {
                return null;
                //TODO: exception...
            }
            // Засовываем идентификатор класса в массив, чтобы отправить его в функцию получения списка классов.
            string[] journalIds = { journalId };
            // Возвращаем список работ.
            Works works = await ByJournalsIdsAsync(api_token, journalIds, onlyHeaders);
            // Возвращаем класс.
            return works;
        }
    }
}
