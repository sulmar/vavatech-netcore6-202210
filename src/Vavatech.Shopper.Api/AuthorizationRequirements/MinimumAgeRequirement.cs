﻿using Microsoft.AspNetCore.Authorization;

namespace Vavatech.Shopper.Api.AuthorizationRequirements
{
    public record MinimumAgeRequirement(int MinimumAge) : IAuthorizationRequirement; // marked interface

    
}
