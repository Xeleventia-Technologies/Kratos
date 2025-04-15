using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Options;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Auth.RefreshTokens;

public class Service(
    [FromServices] IRepository repo,
    [FromServices] ITokenService tokenService,
    [FromServices] IOptions<JwtOptions> jwtOptions,
    [FromServices] UserManager<User> userManager
)
{
    public async Task<Result<GeneratedTokens>> GenerateNewTokensAsync(long userId, string refreshToken, string sessionId, CancellationToken cancellationToken)
    {
        UserSession? userSession = await repo.GetSessionForUserAsync(userId, sessionId, cancellationToken);
        if (userSession is null || userSession.RefreshToken != refreshToken || userSession.RefreshTokenExpiresAt < DateTime.UtcNow)
        {
            return Result.UnauthorizedError();
        }

        IList<string> userRoles = await userManager.GetRolesAsync(userSession.User);
        IList<Claim> permissions = await userManager.GetClaimsAsync(userSession.User);

        string newAccessToken = tokenService.GenerateAccessToken(userSession.User, [.. userRoles], [.. permissions]);
        string newRefreshToken = tokenService.GenerateRefreshToken();
        DateTime newRefreshTokenExpiry = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays);

        userSession.RefreshToken = newRefreshToken;
        userSession.RefreshTokenExpiresAt = newRefreshTokenExpiry;

        await repo.UpdateRefreshToken(userSession, cancellationToken);
        return Result.Success(new GeneratedTokens(newAccessToken, newRefreshToken));
    } 
}