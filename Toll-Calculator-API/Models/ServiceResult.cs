using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Toll_Calculator_API.Models
{
    public class ServiceResult<T>
    {
        public Exception Exception { get; set; }
        public T Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ServiceResult(Exception ex, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            Exception = ex;
            StatusCode = statusCode;
        }

        public ServiceResult(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Exception = null;
            Result = result;
            StatusCode = statusCode;
        }

        public ServiceResult(Exception ex, T result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Exception = ex;
            Result = result;
            StatusCode = statusCode;
        }

        public ServiceResult()
        {
        }

        public ObjectResult ToHttpResult()
        {
            if (StatusCode == HttpStatusCode.OK)
                return new ObjectResult(Result) { StatusCode = (int)StatusCode };

            return new ObjectResult(Exception.Message) { StatusCode = (int)StatusCode };
        }

        public bool IsSuccessStatusCode()
        {
            return StatusCode == HttpStatusCode.OK;
        }
    }
}
