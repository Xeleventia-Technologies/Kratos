using System.Security.Claims;

using Kratos.Api.Common.Constants;
using CustomClaim = Kratos.Api.Common.Constants.Claim;

namespace Kratos.Api.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long GetUserId(this ClaimsPrincipal claims)
    {
        string id = claims.FindFirstValue(CustomClaim.UserId.Name)!;
        return long.Parse(id);
    }

    public static string GetSessionId(this ClaimsPrincipal claims)
    {
        return claims.FindFirstValue(TokenType.SessionId.Name)!;
    }
}
