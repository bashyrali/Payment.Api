using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace PayApp.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
                
            }
        }

        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    Log.Error("Validation error: {V}",validationException.Errors);
                    break;
                case KeyNotFoundException keyNotFoundException:
                    code = HttpStatusCode.BadRequest;
                    Log.Error("Validation error(key not found): {V}", keyNotFoundException.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            if (result == String.Empty)
            {
                Log.Error("Error:{E}",exception.Message);
                result = JsonSerializer.Serialize(new {eror = exception.Message});
            }

            return context.Response.WriteAsync(result);
        }
    }
}