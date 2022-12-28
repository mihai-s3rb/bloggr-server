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
                await HandleException(httpContext, ex.Message, 404, null);
            }
            catch (CustomException ex)
            {
                await HandleException(httpContext, "An internal server error occured", 500, ex.Errors);
            }

        }

        private async Task HandleException(HttpContext httpContext, string message, int statusCode, IEnumerable<string>? errors)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var errorResponse = new ErrorModel
            {
                Message = message,
            };
            if (errors != null)
                foreach (var error in errors)
                {
                    var errorModel = new ErrorModel
                    {
                        Message = error
                    };
                    errorResponse.Errors.Add(errorModel);

                }
            
            await httpContext.Response.WriteAsync(errorResponse.ToString());
        }
    }
}
