using DotNetClaim = System.Security.Claims.Claim;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

using Kratos.Api.Features.Auth.Token;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Common.Types;

using static Kratos.Api.Common.Constants.Auth;

namespace Kratos.Api.Features.Auth.Login;

public class Service(
    [FromServices] UserManager<User> userManager,
    [FromServices] SignInManager<User> signInManager,
    [FromServices] IRepository repo,
    [FromServices] ITokenService tokenService
)
{
    public async Task<Result<LoginResult>> LoginAsync(Request request, CancellationToken cancellationToken)
    {
        User? foundUser = await userManager.FindByEmailAsync(request.Email);
        if (foundUser is null || await userManager.IsLockedOutAsync(foundUser))
        {
            return Result.UnauthorizedError("Wrong email or password");
        }

        if (!await userManager.IsEmailConfirmedAsync(foundUser))
        {
            return Result.UnauthorizedError("Email is not verified");
        }

        SignInResult result = await signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Result.UnauthorizedError("Wrong email or password");
        }

        IList<string> userRoles = await userManager.GetRolesAsync(foundUser);
        IList<DotNetClaim> permissions = await userManager.GetClaimsAsync(foundUser);

        string accessToken = tokenService.GenerateAccessToken(foundUser, [.. userRoles], [.. permissions]);
        string refreshToken = tokenService.GenerateRefreshToken();
        string sessionId = tokenService.GenerateSessionId();

        UserToken? userToken = request.SessionId is not null ? await repo.GetUserTokenAsync(foundUser.Id, request.SessionId, cancellationToken) : null;
        userToken ??= new()
        {
            UserId = foundUser.Id,
            SessionId = sessionId,
            LoginProvider = LoginProvider.Self.Name
        };

        userToken.Value = refreshToken;
        await repo.AddOrUpdateUserTokenAsync(userToken, cancellationToken);

        LoginResult response = new()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            SessionId = sessionId,
        };

        return Result.Success(response);
    }
}
