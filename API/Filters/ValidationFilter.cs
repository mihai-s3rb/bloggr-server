using Bloggr.Application.Validators.Post;
using Bloggr.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bloggr.WebUI.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                var argType = argument?.GetType();
                //check if is a dto
                if (argType == null || !argType.IsClass || argType.IsGenericType)
                    continue;
                //get the abstract validator based on the type of the dto incoming
                var type = typeof(AbstractValidator<>).MakeGenericType(argType);
                //get the validator from the container
                var validator = context.HttpContext.RequestServices.GetService(type) as IValidator;

                if (validator != null)
                {
                    //create the validation context and validate
                    IValidationContext validationContext = Activator.CreateInstance(typeof(ValidationContext<>).MakeGenericType(argType), argument) as IValidationContext;
                    var result = await validator.ValidateAsync(validationContext);
                    //if errs addd them to model state
                    foreach (var error in result.Errors)
                    {
                        context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(p => p.Key, p => p.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                var errorResponse = new ErrorModel
                {
                    Message = "Validation error"
                };
                foreach (var error in errors)
                {
                    foreach (var subError in error.Value)
                    {
                        var errorModel = new ErrorModel
                        {
                            Message = subError
                        };
                        errorResponse.Errors.Add(errorModel);
                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            await next();
        }
    }
}
