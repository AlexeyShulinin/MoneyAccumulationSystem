using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using MoneyAccumulationSystem.CrossCutting.Auth;

namespace MoneyAccumulationSystem.CrossCutting.Extensions;

public static class HttpContextAccessorExtensions
{
    public static AuthUser GetAuthUserFromClaims(this IHttpContextAccessor httpContextAccessor)
    {
        var authUserClaim = httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == AuthConstants.AuthUserClaim);

        if (authUserClaim == null)
        {
            return new AuthUser();
        }
        
        return JsonSerializer.Deserialize<AuthUser>(authUserClaim.Value);
    }
}