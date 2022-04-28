﻿using nsAPI.Entities;
using System;
using System.Net;
using System.Net.Http;

namespace Helpers
{
    public enum TypeOperation
    {
        // USERS
        USERS_GET = 100,
        USERS_DELETE = 101,
        USERS_AUTH = 102,
        USERS_REG = 103,
        // CLASSES
        CLASSES_GET = 200,
        // JOURNALS
        JOURNALS_GET = 300,
        // TESTS
        TESTS_GET = 400,
        // CLIENT
        CLIENT_AUTH = 700,
        // References Books
        GENDERS_GET = 600,

        OTHER,
    }
    class HttpResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Content { get; private set; }
        public HttpResponseException(HttpResponseMessage responseMessage) : base(responseMessage.ReasonPhrase)
        {
            StatusCode = responseMessage.StatusCode;
            Content = responseMessage.Content.ReadAsStringAsync().Result;
        }
    }

    /// <summary>
    /// Исключение, возникающее, когда сервер вернул пустой ответ.
    /// </summary>
    class EmptyHttpResponseException : Exception
    {
        public EmptyHttpResponseException() : base("От сервера пришел пустой ответ") { }
    }

    /// <summary>
    /// Исключение, возникающее, когда сервер вернул пустой ответ.
    /// </summary>
    class ErrorResponseException : Exception
    {
        public int ErrCode { get; private set; }
        public string Description { get; private set; }
        public string Additional { get; private set; }
        public ErrorResponseException(Error error, TypeOperation typeOperation) :
            base("От сервера пришел ответ об ошибке " + error.errorInfo.Additional)
        {
            ErrCode = error.errorInfo.Type;
            Description = error.errorInfo.Description;
            Additional = error.errorInfo.Message;
        }
    }

    class UnknownHttpResponseException : Exception
    {
        public string ResponseJSON;
        public UnknownHttpResponseException(string responseJson) : base("Получен ответ неизвестного формата!")
        {
            ResponseJSON = responseJson;
#if DEBUG
            Log.Write(responseJson);
#endif
        }
    }

    class FindUserException : Exception
    {
        public FindUserException(string msg) : base(msg) { }
    }
}
