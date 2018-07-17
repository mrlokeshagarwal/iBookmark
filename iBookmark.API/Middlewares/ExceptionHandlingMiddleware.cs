using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace iBookmark.API.Middlewares
{
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _emailClaimKey;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            using (var memory = new MemoryStream())
            {
                var originalStream = context.Response.Body;
                context.Response.Body = memory;
                var messages = new List<string>() { };
                try
                {
                    await _next(context);
                }

                catch (AggregateException aex)
                {
                    foreach (var aexInnerException in aex.InnerExceptions)
                    {
                        messages.AddRange(await HandleExceptionAsync(context, aexInnerException).ConfigureAwait(false));
                    }
                    CopyErrortoResponse(context, memory, originalStream, messages);
                }
                catch (Exception ex)
                {
                    messages.AddRange(await HandleExceptionAsync(context, ex));
                    CopyErrortoResponse(context, memory, originalStream, messages);
                }
                memory.Seek(0, SeekOrigin.Begin);
                memory.CopyTo(originalStream);
            }
        }
        private void CopyErrortoResponse(HttpContext context, MemoryStream memory, Stream originalStream, List<string> messages)
        {
            memory.Seek(0, SeekOrigin.Begin);
            var json = JsonConvert.SerializeObject(new { errors = messages });
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));
            stream.CopyTo(memory);
            context.Response.Body = originalStream;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
        }
        private async Task<List<string>> HandleExceptionAsync(HttpContext context, Exception exception)
        {
            List<string> errors = new List<string>();
            try
            {
                var result = string.Empty;
                var baseException = exception.GetBaseException();
                if (baseException.GetType() == typeof(SqlException))
                {
                    var ex = (SqlException)baseException;
                    for (int loop = 0; loop < ex.Errors.Count; loop++)
                    {
                        if (ex.Errors[0].Number == 50000)
                            errors.Add(ex.Message);
                    }
                }
                else
                {
                    errors.Add("There is some error while performing this action. please try again later");
                }
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
            return errors;
        }
    }

}
