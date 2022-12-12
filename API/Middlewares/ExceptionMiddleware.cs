using Bloggr.Application.Models;
using Bloggr.Domain.Exceptions;
using System.Net;

namespace Bloggr.WebUI.Middlewares
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
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }

        }

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {

            var errorDetails = new { message = "", statusCode = 0 };
            errorDetails = exception switch
            {
                EntityNotFoundException => (new {
                    message = exception.Message,
                    statusCode = 404
                }),
                _ => (new
                {
                    message = "An internal server error has occured",
                    statusCode = 500
                })
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)errorDetails.statusCode;

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = errorDetails.message
            }.ToString());
        }
    }
}
