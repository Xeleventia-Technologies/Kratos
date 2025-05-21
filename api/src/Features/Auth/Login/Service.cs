using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Auth.Login;

public class Service(
    [FromServices] IRepository repo,
    [FromServices] ITokenService tokenService,
    [FromServices] UserManager<User> userManager,
    [FromServices] SignInManager<User> signInManager
)
{
    public async Task<Result<LoggedInUser>> LoginAsync(string email, string password, string? sessionId, CancellationToken cancellationToken)
    {
        User? foundUser = await userManager.FindByEmailAsync(email);
        if (foundUser is null || await userManager.IsLockedOutAsync(foundUser))
        {
            return Result.UnauthorizedError("Wrong email or password");
        }

        bool emailConfirmed = await userManager.IsEmailConfirmedAsync(foundUser);
        if (!emailConfirmed)
        {
            return Result.UnauthorizedError("Email is not verified");
        }

        SignInResult signInResult = await signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
        if (!signInResult.Succeeded)
        {
            return Result.UnauthorizedError("Wrong email or password");
        }

        Profile? profile = await repo.GetUserProfileAsync(foundUser.Id, cancellationToken);
        GeneratedTokens generatedTokens = await tokenService.GenerateAndSaveAuthTokensAsync(sessionId, foundUser, cancellationToken);

        LoggedInUser loggedInUser = new(
            foundUser.Id,
            profile?.FullName ?? "[No name set]",
            foundUser.Email!,
            profile?.DisplayPictureFileName,
            generatedTokens.AccessToken,
            generatedTokens.RefreshToken,
            generatedTokens.SessionId
        );

        return Result.Success(loggedInUser);
    }
}
