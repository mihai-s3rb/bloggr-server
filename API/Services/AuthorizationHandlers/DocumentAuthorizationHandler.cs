using Bloggr.Application.Models;
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
            }

            return Task.CompletedTask;
        }
    }
}

public class SameAuthorRequirement : IAuthorizationRequirement { }