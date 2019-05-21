using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PersonManagement.Contracts;

namespace Uebung_RestAPI.ExceptionMiddleware
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
           
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if(ex is DataNotFoundException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

          var result = new ExceptionDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = $"An error occured while progressing the request {httpContext.Request.Path}",
                Exception = ex,
                InnerException = ex.InnerException
            };
                return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }

    }


}
