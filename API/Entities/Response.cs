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
        public List<object> Data { get; set; }
        //public Dictionary<string, object> data { get; set; }

        public Response()
        {
            Data = null;
        }

        /// <summary>
        ///Ответ с ошибкой определяемого типа.
        /// </summary>
        /// <param name="typeError">Тип ошибки.</param>
        /// <param name="message">Сообщение об ошибке.</param>
        public Response(TError typeError, string message)
        {
            Data = null;
            Exception = new ResponseError(typeError, message);
        }

        /// <summary>
        /// Создаёт ответ, содержащий только ошибку сервера.
        /// </summary>
        /// <param name="ResponseCode"></param>
        /// <param name="message"></param>
        public Response(HttpResponseMessage httpResponseMessage)
        {
            Data = null;
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
                Data = null;
            }
            else
            // Если вернулась ошибка.
            if (JSONHelper.IsError(httpResponse))
            {
                // ошибка.
                Exception = new ResponseError(TError.DefinedError, httpResponse);
                Data = null;
            }
            else if (JSONHelper.IsResponse(httpResponse))
            {
                // Конвертируем строку JSON в тип response.
                var t = FromJson(httpResponse);
                Data = t.Data;
            }
            // Если полученная строка незнакома.
            else
            {
                Exception = new ResponseError(TError.UnknownError, httpResponse);
                Data = null;
            }
        }

        public static Response FromJson(string json) => JsonConvert.DeserializeObject<Response> (json, Converter.Settings);

        //=============================================================================
        [JsonIgnore()]
        public readonly ResponseError Exception;
    }
    /// <summary>
    /// Тип API-ошибки.
    /// </summary>
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

    /// <summary>
    /// Тип ошибки.
    /// </summary>
    public enum TError
    {
        /// <summary>
        /// Не известная ошибка.
        /// </summary>
        UnknownError,
        /// <summary>
        /// Пустой ответ.
        /// </summary>
        Empty,
        /// <summary>
        /// Известная ошибка.
        /// </summary>
        DefinedError,
        /// <summary>
        /// Ошибка сервера - типа 404 - не найден ресурс и т.д.
        /// </summary>
        ServerError,
        /// <summary>
        /// Ошибка со связью.
        /// </summary>
        ConnectError,
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
        /// <summary>
        /// Ответ от сервера, который не удалось распознать.
        /// </summary>
        public string ServerText { get; set; } = null;
        /// <summary>
        /// Ошибка при соединении.
        /// </summary>
        public string SocketText { get; set; } = null;
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
                else if (ServerText != null)
                {
                    return ServerText;
                }
                else if (SocketText != null)
                {
                    return SocketText;
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

        public ResponseError(TError typeError, string errorMessage)
        {
            if (typeError == TError.UnknownError)
            {
                TypeError = typeError;
                ServerText = errorMessage;
            }
            else if (typeError == TError.ConnectError)
            {
                TypeError = typeError;
                SocketText = errorMessage;
            }
            else
            {
                // Произошла ошибка при попытке получения информации из БД.
                Error error = Error.FromJson(errorMessage);
                ErrorInfo = error.errorInfo;
                Code = (CODE_ERROR)error.errorInfo.Type;
                TypeError = typeError;
            }
        }

    }
}

