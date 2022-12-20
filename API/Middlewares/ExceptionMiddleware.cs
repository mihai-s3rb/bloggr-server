using Bloggr.Domain.Models;
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
            //catch (Exception ex)
            //{
            //    await HandleException(httpContext, "An internal server error occured", 500);
            //}

        }

        private async Task HandleException(HttpContext httpContext, string message, int statusCode)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(new ErrorModel()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
