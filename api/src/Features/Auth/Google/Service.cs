using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

using Google.Apis.Auth;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Options;
using Kratos.Api.Common.Services;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Auth.Google;

public class Service(
    [FromServices] ITokenService tokenService,
    [FromServices] IOptions<OAuthOptions> oauthOptions,
    [FromServices] UserManager<User> userManager
)
{
    public async Task<Result<GeneratedTokens>> LoginWithGoogleAsync(Request request, CancellationToken cancellationToken)
    {
        Result<GoogleUser> googleUserResult = await VerifyGoogleTokenAsync(request.GoogleToken);
        if (!googleUserResult.IsSuccess)
        {
            return Result.UnauthorizedError();
        }

        GoogleUser googleUser = googleUserResult.Value;
        User? user = await userManager.FindByEmailAsync(googleUser.Email);

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
        }

        GeneratedTokens generatedTokens = await tokenService.GenerateAndSaveAuthTokensAsync(request.SessionId, user, cancellationToken);
        return Result.Success(generatedTokens);
    }

    private async Task<Result<GoogleUser>> VerifyGoogleTokenAsync(string googleToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings { Audience = [oauthOptions.Value.GoogleClientId] };
            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(googleToken, settings);

            if (payload.Email is null)
            {
                return Result.Fail("Failed to verify Google token.");
            }

            GoogleUser googleUser = new(payload.Subject, payload.Email, payload.Name, payload.Picture);
            return Result.Success(googleUser);
        }
        catch (InvalidJwtException ex)
        {
            return Result.Fail($"Failed to verify Google token. Reason: {ex.Message}");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to verify Google token. Reason: {ex.Message}");
        }
    }
}
