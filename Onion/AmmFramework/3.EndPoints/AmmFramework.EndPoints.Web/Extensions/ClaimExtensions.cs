﻿using System.Security.Claims;

namespace AmmFramework.EndPoints.Web.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetClaim(this ClaimsPrincipal userClaimsPrincipal, string claimType)
        {
            return userClaimsPrincipal.Claims?.FirstOrDefault((x) => x.Type == claimType)?.Value;
        }
    }
}