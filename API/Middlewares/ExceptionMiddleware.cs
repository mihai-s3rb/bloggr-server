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
            } catch(EntityNotFoundException ex)
            {
                await HandleException(httpContext, ex.Message, 404);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, "An internal server error occured", 500);
            }

        }

        private async Task HandleException(HttpContext httpContext, string message, int statusCode)
        {

            //var errorDetails = new { message = "", statusCode = 0 };
            //errorDetails = exception switch
            //{
            //    EntityNotFoundException => (new {
            //        message = exception.Message,
            //        statusCode = 404
            //    }),
            //    _ => (new
            //    {
            //        message = "An internal server error has occured",
            //        statusCode = 500
            //    })
            //};
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
