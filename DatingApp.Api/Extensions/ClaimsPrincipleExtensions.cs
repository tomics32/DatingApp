﻿using System.Security.Claims;

namespace DatingApp.Api.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var username = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (username == null)
        {
            throw new Exception("Cannot get username from token");
        }
        return username;
    }
}
