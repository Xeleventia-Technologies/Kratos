using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Extensions;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Auth.Google;

public class Service(
    [FromServices] ITokenService tokenService,
    [FromServices] IGoogleTokenService googleTokenService,
    [FromServices] UserManager<User> userManager
)
{
    public async Task<Result<GeneratedTokens>> LoginWithGoogleAsync(string googleToken, string? sessionId, CancellationToken cancellationToken)
    {
        Result<GoogleUser> googleUserResult = await googleTokenService.VerifyGoogleTokenAsync(googleToken);
        if (!googleUserResult.IsSuccess)
        {
            return Result.UnauthorizedError();
        }

        GoogleUser googleUser = googleUserResult.Value;
        User? user = await userManager.FindByEmailAsync(googleUser.Email);

        bool newUser = false;
        if (user is null)
        {
            user = new() { Email = googleUser.Email, UserName = googleUser.Email };

            IdentityResult createUserResult = await userManager.CreateAsync(user);
            if (!createUserResult.Succeeded)
            {
                return Result.CannotProcessError($"Failed to create user. Reason: {createUserResult.Errors.CommaSeparated()}");
            }

            IdentityResult addToRoleResult = await userManager.AddToRoleAsync(user, Common.Constants.Role.User.Name);
            if (!addToRoleResult.Succeeded)
            {
                return Result.CannotProcessError($"Failed to assign role to user. Reason: {addToRoleResult.Errors.CommaSeparated()}");
            }

            newUser = true;
        }

        GeneratedTokens generatedTokens = await tokenService.GenerateAndSaveAuthTokensAsync(sessionId, user, cancellationToken);
        return Result.Success(
            value: generatedTokens,
            successStatus: newUser ? SuccessStatus.Created : SuccessStatus.Ok
        );
    }
}
