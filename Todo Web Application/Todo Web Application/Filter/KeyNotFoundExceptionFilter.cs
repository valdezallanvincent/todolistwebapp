using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TodoWebApplication.Service;

namespace TodoWebApplication.Filter
{
    public class KeyNotFoundExceptionFilter : IExceptionFilter
    {
        public async void OnException(ExceptionContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            if (context.Exception is KeyNotFoundException)
            {
                var response = new ResponseErrorModel(404, context.Exception.Message);
                var responseJson = JsonConvert.SerializeObject(response);
                var responseBytes = Encoding.UTF8.GetBytes(responseJson);

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.ContentLength = responseBytes.Length;
                await HttpResponseWritingExtensions.WriteAsync(context.HttpContext.Response, responseJson);

                context.ExceptionHandled = true;
                return;
            }
        }
    }
}
