using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TodoWebApplication.Exceptions;
using TodoWebApplication.Service;

namespace TodoWebApplication.Filter
{
    public class ConflictFoundExceptionFilter : IExceptionFilter
    {
        public async void OnException(ExceptionContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            if (context.Exception is ConflictException)
            {
                var errorException = (ConflictException)context.Exception;

                var response = errorException.ResponseErrorModel;
                var responseJson = JsonConvert.SerializeObject(response);
                var responseBytes = Encoding.UTF8.GetBytes(responseJson);

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.ContentLength = responseBytes.Length;
                await HttpResponseWritingExtensions.WriteAsync(context.HttpContext.Response, responseJson);

                context.ExceptionHandled = true;
                return;
            }
        }
    }
}
