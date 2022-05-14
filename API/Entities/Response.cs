using Newtonsoft.Json;
using nsAPI.JSON;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace nsAPI.Entities
{
    public class Response
    {
        [JsonProperty("response")]
        public List<object> data { get; set; }
        //public Dictionary<string, object> data { get; set; }

        public Response()
        {
            data = new List<object>();
        }

        /// <summary>
        /// Создаёт ответ, содержащий только ошибку сервера.
        /// </summary>
        /// <param name="ResponseCode"></param>
        /// <param name="message"></param>
        public Response(HttpResponseMessage httpResponseMessage)
        {
            data = null;
            Exception = new ResponseError(httpResponseMessage);
        }

        /// <summary>
        /// Получить объект Response из JSON-строки.
        /// </summary>
        /// <param name="json">Строка в формате JSON</param>
        /// <returns></returns>
        public Response(string httpResponse)
        {
            // Пришёл пустой ответ от сервера.
            if (string.IsNullOrEmpty(httpResponse))
            {
                Exception = new ResponseError();
                data = null;
            }
            else
            // Если вернулась ошибка.
            if (JSONHelper.IsError(httpResponse))
            {
                // Вызываем исключение.
                Exception = new ResponseError(TError.DefinedError, httpResponse);
                data = null;
            }
            else if (JSONHelper.IsResponse(httpResponse))
            {
                // Конвертируем строку JSON в тип response.
                var t = FromJson(httpResponse);
                data = t.data;
            }
            // Если полученная строка незнакома.
            else
            {
                Exception = new ResponseError(TError.UnknownError);
                data = null;
            }
        }

        public static Response FromJson(string json) => JsonConvert.DeserializeObject<Response> (json, Converter.Settings);

        //=============================================================================
        [JsonIgnore()]
        public readonly ResponseError Exception;
    }

    public enum CODE_ERROR
    {
        ERR_NotEnoughInf = 100,
        ERR_UserAlreadyReg = 101,
        ERR_UserNotFound = 102,
        ERR_DBNotAvailable = 103,
        ERR_SecureKeyProblem = 104,
        ERR_DBProblem = 105,
        ERR_ClassAlreadyExist = 106,
        ERR_UserAlreadyAssociatedWClass = 107,
        ERR_RefbookNotFound = 108,
        ERR_ClassNotFound = 109,
        ERR_WorksNotFound = 110,
        ERR_WorkAlreadyReg = 111,
        ERR_AnswerNotFound = 112,
        ERR_AnswerAlreadyReg = 113,
        ERR_MarkAlreadyExist = 114,
        ERR_IsTooLong = 115,
        ERR_WordAlreadyExist = 116,
    }

    public enum TError
    {
        UnknownError,
        Empty,
        DefinedError,
        ServerError,
    }


    public class ResponseError
    {
        public HttpResponseMessage HttpResponseMessage { get; set; }
        /// <summary>
        /// Тип ошибки.
        /// </summary>
        public TError TypeError { get; set; }
        /// <summary>
        /// Код ошибки от API.
        /// </summary>
        public CODE_ERROR Code { get; set; }
        /// <summary>
        /// Информация об ошибке от API.
        /// </summary>
        public ErrorInfo ErrorInfo { get; set; }

        public string Message
        {
            get
            {
                if (HttpResponseMessage!=null)
                {
                    string contentMsg = HttpResponseMessage.Content.ReadAsStringAsync().Result;
                    return HttpResponseMessage.StatusCode.ToString() + " - " +
                        HttpResponseMessage.ReasonPhrase + ". Content: " + contentMsg??"empty";
                }
                else if (ErrorInfo != null)
                {
                    return "Message: " + ErrorInfo.Message + "\n" +
                        "Description: " + ErrorInfo.Description + "\n" +
                        "Additional" + ErrorInfo.Additional + "\n";
                }
                else
                {
                    return "No message";
                }
            }
        }

        public ResponseError(HttpResponseMessage httpResponseMessage)
        {
            TypeError = TError.ServerError;
            HttpResponseMessage = httpResponseMessage;
            //Content.ReadAsStringAsync()
        }

        public ResponseError()
        {
            TypeError = TError.Empty;
        }
        public ResponseError(TError typeError)
        {
            TypeError = typeError;
        }

        public ResponseError(TError typeError, Error error)
        {
            ErrorInfo = error.errorInfo;
            Code = (CODE_ERROR)error.errorInfo.Type;
            TypeError = typeError;
        }

        public ResponseError(TError typeError, string responseErrorMessage)
        {
            // Произошла ошибка при попытке получения информации из БД.
            Error error = Error.FromJson(responseErrorMessage);
            ErrorInfo = error.errorInfo;
            Code = (CODE_ERROR)error.errorInfo.Type;
            TypeError = typeError;
        }

    }
}

