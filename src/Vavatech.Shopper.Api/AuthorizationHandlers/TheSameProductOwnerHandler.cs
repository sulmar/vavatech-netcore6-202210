using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Vavatech.Shopper.Api.AuthorizationRequirements;

namespace Vavatech.Shopper.Api.AuthorizationHandlers
{
    public class TheSameProductOwnerHandler : AuthorizationHandler<TheSameOwnerRequirement, Product>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TheSameOwnerRequirement requirement, Product resource)
        {
            string username = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (resource.Owner == username)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
