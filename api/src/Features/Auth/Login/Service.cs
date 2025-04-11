using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Options;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Features.Auth.Token;

namespace Kratos.Api.Features.Auth.Login;

public class Service(
    [FromServices] IRepository repo,
    [FromServices] ITokenService tokenService,
    [FromServices] UserManager<User> userManager,
    [FromServices] SignInManager<User> signInManager,
    [FromServices] IOptions<JwtOptions> jwtOptions
)
{
    public async Task<Result<LoginResult>> LoginAsync(Request request, CancellationToken cancellationToken)
    {
        User? foundUser = await userManager.FindByEmailAsync(request.Email);
        if (foundUser is null || await userManager.IsLockedOutAsync(foundUser))
        {
            return Result.UnauthorizedError("Wrong email or password");
        }

        bool emailConfirmed = await userManager.IsEmailConfirmedAsync(foundUser);
        if (!emailConfirmed)
        {
            return Result.UnauthorizedError("Email is not verified");
        }

        SignInResult result = await signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Result.UnauthorizedError("Wrong email or password");
        }

        IList<string> userRoles = await userManager.GetRolesAsync(foundUser);
        IList<Claim> permissions = await userManager.GetClaimsAsync(foundUser);

        string accessToken = tokenService.GenerateAccessToken(foundUser, [.. userRoles], [.. permissions]);
        string refreshToken = tokenService.GenerateRefreshToken();
        
        DateTime loggedInAt = DateTime.UtcNow;
        DateTime refreshTokenExpiry = loggedInAt.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays);

        UserSession? userSession = request.SessionId is not null ? await repo.GetUserSessionAsync(foundUser.Id, request.SessionId, cancellationToken) : null;
        if (userSession is null)
        {
            string sessionId = tokenService.GenerateSessionId();
            userSession = new()
            {
                UserId = foundUser.Id,
                SessionId = sessionId,
                LoggedInWith = Enums.LoggedInWith.Email,
                LoggedInAt = loggedInAt,
            };
        }

        userSession.LoggedInAt = loggedInAt;
        userSession.RefreshToken = refreshToken;
        userSession.RefreshTokenExpiresAt = refreshTokenExpiry;

        await repo.AddOrUpdateUserSessionAsync(userSession, cancellationToken);

        LoginResult response = new()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            SessionId = userSession.SessionId,
        };

        return Result.Success(response);
    }
}
