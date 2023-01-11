using Bloggr.Application.Models;
using Bloggr.Domain.Exceptions;
using Domain.Abstracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Bloggr.WebUI.Services.AuthorizationHandlers
{
    public class DocumentAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, DocumentDto>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   SameAuthorRequirement requirement,
                                                   DocumentDto resource)
        {
            var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var resourceId = resource.UserId.ToString();
            if (userId.Equals(resourceId))
            {
                context.Succeed(requirement);
            } else
            {
                throw new NotAuthorizedException("You are not allowed to do this");
            }
            return Task.CompletedTask;
        }
    }
}

public class SameAuthorRequirement : IAuthorizationRequirement { }