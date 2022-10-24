using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Vavatech.Shopper.Api.AuthorizationRequirements;

namespace Vavatech.Shopper.Api.AuthorizationHandlers
{

    public static class MinimumAgeRequirementExtensions
    {
        public static AuthorizationPolicyBuilder RequireMinimumAge(this AuthorizationPolicyBuilder policy, int age)
        {
            policy.RequireClaim(ClaimTypes.DateOfBirth);
            policy.Requirements.Add(new MinimumAgeRequirement(age));

            return policy;
        }
    }

    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {

            string value = context.User.FindFirstValue(ClaimTypes.DateOfBirth);

            DateTime dateOfBirth = Convert.ToDateTime(value);

            int age = DateTime.Today.Year - dateOfBirth.Year;

            // Predykat
            if (age >= requirement.MinimumAge)
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
