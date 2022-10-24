using Microsoft.AspNetCore.Authorization;

namespace Vavatech.Shopper.Api.AuthorizationRequirements
{
    public record TheSameOwnerRequirement : IAuthorizationRequirement;
}
