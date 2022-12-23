using Bloggr.Application.Validators.Post;
using FluentValidation;

namespace Bloggr.WebUI.Extensions
{
    public static class ValidatorsExtension
    {
        public static void AddVaidators(this WebApplicationBuilder builder)
        {
            var validators = typeof(PostValidator).Assembly.GetTypes().Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>));
            foreach (var validator in validators)
            {
                builder.Services.Add(new ServiceDescriptor(validator.BaseType, validator, ServiceLifetime.Scoped));
            }
        }
    }
}
