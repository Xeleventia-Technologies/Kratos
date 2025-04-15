using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Options;
using static Kratos.Api.Common.Constants.Auth;

namespace Kratos.Api.Features.Auth.Login;

public static class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.AsHttpError();
        }

        Result<GeneratedTokens> loginResult = await service.LoginAsync(request, cancellationToken);
        return loginResult.AsHttpResponse();
    }

    public static async Task<IResult> HandleWebAsync(
        HttpResponse httpResponse,
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.AsHttpError();
        }

        Result<GeneratedTokens> result = await service.LoginAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.Error.AsHttpError();
        }

        GeneratedTokens generatedTokens = result.Value!;

        httpResponse.Cookies.Append(TokenType.SessionId.Name, generatedTokens.SessionId, new CookieOptions()
        {
            Path = "/",
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays)
        });

        httpResponse.Cookies.Append(TokenType.AccessToken.Name, generatedTokens.AccessToken, new CookieOptions()
        {
            Path = "/",
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddMinutes(jwtOptions.Value.AccessTokenExpiryInMinutes)
        });

        httpResponse.Cookies.Append(TokenType.RefreshToken.Name, generatedTokens.RefreshToken, new CookieOptions()
        {
            Path = Registry.RefreshTokensWebUrl,
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays)
        });

        return Results.Ok();
    }
}
