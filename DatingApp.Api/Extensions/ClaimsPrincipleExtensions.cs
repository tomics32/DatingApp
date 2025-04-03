using System.Security.Claims;

namespace DatingApp.Api.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var username = user.FindFirstValue(ClaimTypes.Name);
        if (username == null)
        {
            throw new Exception("Cannot get username from token");
        }
        return username;
    }
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            throw new Exception("Cannot get ID of this user from token");
        }

        var userId = int.Parse(userIdClaim);
        return userId;
    }
}
