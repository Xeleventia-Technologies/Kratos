using System.Security.Claims;

using Kratos.Api.Common.Constants;

namespace Kratos.Api.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long GetUserId(this ClaimsPrincipal claims)
    {
        string id = claims.FindFirstValue(Auth.Claim.UserId.Name)!;
        return long.Parse(id);
    }
}
