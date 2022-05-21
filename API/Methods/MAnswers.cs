using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nsAPI.Methods
{
    class MAnswers: Basic
    {
        public async Task<string> TestAnswerAddAsync(string api_token, TestAnswerForAdd answer)
        {
            // Тут Будет храниться результат запроса.
            Identifier id;

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Указание правильного типа работы.
            var wts = API.Instance.Refbooks[TRefbooks.WorkTypes];
            if (wts != null)
            {
                Refbook worktype = wts.FirstOrDefault(wt => wt.Name == "Тест");
                answer.AnswerHeader.id_TypeWork = (worktype?.ID) ?? "1";
            }
            else answer.AnswerHeader.id_TypeWork = "1";
            var answToSend = answer.Clone();
            // Получаем пользователя с зашифрованными данными.
            answToSend.EncryptByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string answerJson = answToSend.ToJson();
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("answers.add/", answerJson, urlParams);
            // Конвертируем данные из нулевой ячейки массива ответа в тип IdentifierClassroom.
            if (httpResponse.Data != null)
            {
                id = Identifier.FromJson(httpResponse.Data[0].ToString());
                // Расшифровываем.
                id.DecryptByAES();
                //
                return id.ID;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> TextAnswerAddAsync(string api_token, TextAnswerForAdd answer)
        {
            // Тут Будет храниться результат запроса.
            Identifier id;

            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Указание правильного типа работы.
            var wts = API.Instance.Refbooks[TRefbooks.WorkTypes];
            if (wts != null)
            {
                Refbook worktype = wts.FirstOrDefault(wt => wt.Name == "Письменная работа");
                answer.AnswerHeader.id_TypeWork = (worktype?.ID) ?? "2";
            }
            else answer.AnswerHeader.id_TypeWork = "2";

            // Получаем пользователя с зашифрованными данными.
            answer.EncryptByAES();

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string answerJson = answer.ToJson();
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("answers.add/", answerJson, urlParams);
            if (httpResponse.Data != null)
            {
                // Конвертируем данные из нулевой ячейки массива ответа в тип IdentifierClassroom.
                id = Identifier.FromJson(httpResponse.Data[0].ToString());
                // Расшифровываем.
                id.DecryptByAES();
                //
                return id.ID;
            }
            else
            {
                return null;
            }
        }


        public async Task<Answers> ByUserIdAsync(string api_token, string id_User, bool onlyHeaders = true)
        {
            if (string.IsNullOrWhiteSpace(id_User))
            {
                return null;
            }
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParam = new Dictionary<string, string>();
            urlParam.Add("secure_key", api_token);
            urlParam.Add("onlyHeaders", onlyHeaders ? "1" : "0");
            // Добавление в массив ID пользователя.
            urlParam.Add("id_User", id_User);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpGetAsync("answers.byuser", urlParam);

            if (httpResponse.Data != null)
            {
                // Список работ.
                Answers answers = new Answers();
                //
                httpResponse.Data.ForEach(el =>
                {
                    JObject answer = JsonConvert.DeserializeObject<JObject>(el.ToString());
                    if (answer.ContainsKey("AnswerHeader"))
                    {
                        // Получение заголовка.
                        AnswerHeader answerHeader = JsonConvert.DeserializeObject<AnswerHeader>(answer["AnswerHeader"].ToString());
                        // Расшифровка заголовка.
                        answerHeader.DecryptByAES();
                        // Test
                        if (answerHeader.id_TypeWork == "1")
                        {
                            //
                            TestAnswer testAnswer = new TestAnswer();
                            //
                            testAnswer.AnswerHeader = answerHeader;
                            if (answer.ContainsKey("AnswerBody"))
                            {
                                testAnswer.AnswerBody = JsonConvert.DeserializeObject<List<TestAnswerBody>>(answer["AnswerBody"].ToString());
                                testAnswer.DecryptBodyByAES();
                            }
                            // Сохраняем работу.
                            answers.TestAnswers.Add(testAnswer);
                        }// Text
                        else if (answerHeader.id_TypeWork == "2")
                        {
                            //
                            TextAnswer textAnswer = new TextAnswer();
                            textAnswer.AnswerHeader = answerHeader;
                            if (answer.ContainsKey("AnswerBody"))
                            {
                                textAnswer.AnswerBody = JsonConvert.DeserializeObject<List<TextAnswerBody>>(answer["AnswerBody"].ToString());
                                textAnswer.DecryptBodyByAES();
                            }
                            // Сохраняем работу.
                            answers.TextAnswers.Add(textAnswer);
                        }
                        else
                        {
                            throw new Exception("При получении ответов на работу не задан тип работы!");
                        }
                    }
                });
                // Возвращаем список классов.
                return answers;
            }
            else
            {
                return null;
            }
        }

        public async Task<Answers> ByWorksIdsAsync(string api_token, string[] worksIDs, bool onlyHeaders = true)
        {
            if (worksIDs == null || worksIDs.Count() <= 0)
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
            d.Add("ids", worksIDs);
            // Сериализация (конвертирование в формат JSON).
            string dataJSON = JsonConvert.SerializeObject(d);
            // Получаем ответ от сервера в виде строки. В строке должен быть ответ в формате JSON.
            var httpResponse = await httpPostJSONAsync("answers.get/", dataJSON, urlParam);

            if (httpResponse.Data != null)
            {
                // Список работ.
                Answers answers = new Answers();
                //
                httpResponse.Data.ForEach(el =>
                {
                    JObject answer = JsonConvert.DeserializeObject<JObject>(el.ToString());
                    if (answer.ContainsKey("AnswerHeader"))
                    {
                        // Получение заголовка.
                        AnswerHeader answerHeader = JsonConvert.DeserializeObject<AnswerHeader>(answer["AnswerHeader"].ToString());
                        // Расшифровка заголовка.
                        answerHeader.DecryptByAES();
                        // Test
                        if (answerHeader.id_TypeWork == "1")
                        {
                            //
                            TestAnswer testAnswer = new TestAnswer();
                            //
                            testAnswer.AnswerHeader = answerHeader;
                            if (answer.ContainsKey("AnswerBody"))
                            {
                                testAnswer.AnswerBody = JsonConvert.DeserializeObject<List<TestAnswerBody>>(answer["AnswerBody"].ToString());
                                testAnswer.DecryptBodyByAES();
                            }
                            // Сохраняем работу.
                            answers.TestAnswers.Add(testAnswer);
                        }// Text
                        else if (answerHeader.id_TypeWork == "2")
                        {
                            //
                            TextAnswer textAnswer = new TextAnswer();
                            textAnswer.AnswerHeader = answerHeader;
                            if (answer.ContainsKey("AnswerBody"))
                            {
                                textAnswer.AnswerBody = JsonConvert.DeserializeObject<List<TextAnswerBody>>(answer["AnswerBody"].ToString());
                                textAnswer.DecryptBodyByAES();
                            }
                            // Сохраняем работу.
                            answers.TextAnswers.Add(textAnswer);
                        }
                        else
                        {
                            throw new Exception("При получении ответов на работу не задан тип работы!");
                        }
                    }
                });
                // Возвращаем список классов.
                return answers;
            }
            else
            {
                return null;
            }
        }

        public async Task<Answers> ByWorkIdAsync(string api_token, string workId, bool onlyHeaders = true)
        {
            if (string.IsNullOrWhiteSpace(workId))
            {
                return null;
                //TODO: exception...
            }
            // Засовываем идентификатор класса в массив, чтобы отправить его в функцию получения списка классов.
            string[] workIDs = { workId };
            // Возвращаем список работ.
            Answers works = await ByWorksIdsAsync(api_token, workIDs, onlyHeaders);
            // Возвращаем класс.
            return works;
        }

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="mark">Оценка в строком типе. Теоретически можно задать до 4 символов, но зачем?</param>
        /// <param name="IDExecutionOfWork">ID ответа - ID записи из таблицы ExectionOfWork</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> SetMark(string api_token, string mark, string IDExecutionOfWork)
        {
            // Обязательно добавляем в запрос НЕ зашифрованный ключ доступа.
            Dictionary<string, string> urlParams = new Dictionary<string, string>();
            urlParams["secure_key"] = api_token;

            // Получаем пользователя с зашифрованными данными.
            Dictionary<string, string> PostParams2 = new Dictionary<string, string>();
            PostParams2["Mark"] = Encryption.AESHelper.EncryptString(mark);
            PostParams2["id_ExecutionOfWork"] = Encryption.AESHelper.EncryptString(IDExecutionOfWork);

            // ВСЕГДА, ПРИ ОТПРАВКЕ POST-ЗАПРОСА, НАДО ДОБАВЛЯТЬ В КОНЦЕ АДРЕСА СЛЭШ!

            // Конвертируем объект в строку в формате JSON.
            string postParamsJson = JsonConvert.SerializeObject(PostParams2);
            Console.WriteLine(postParamsJson);
            // Отправляем на сервер.
            var httpResponse = await httpPostJSONAsync("mark.add/", postParamsJson, urlParams);
            if (httpResponse.Data != null)
            {
                // Ответ.
                return httpResponse.Data[0].ToString() == "OK";
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="header">Заголовок ответа на работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> SetMark(string api_token, AnswerHeader header) => 
            await SetMark(api_token, header.Mark, header.ID);

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="testAnswer">Ответ на тестову работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> SetMark(string api_token, TestAnswer testAnswer) =>
            await SetMark(api_token, testAnswer.AnswerHeader.Mark, testAnswer.AnswerHeader.ID);

        /// <summary>
        /// Задает оценку указанному ответу.
        /// </summary>
        /// <param name="api_token">Ключ для доступа к АПИ.</param>
        /// <param name="textAnswer">Ответ на письменную работу. В нем содержатся необходимые данные для отправки оценки.</param>
        /// <returns>True - при успешном добавлении оценки. В пртивном случае False не вернет, а выкинет исключение.</returns>
        public async Task<bool> SetMark(string api_token, TextAnswer textAnswer) =>
            await SetMark(api_token, textAnswer.AnswerHeader.Mark, textAnswer.AnswerHeader.ID);
    }
}
