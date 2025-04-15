using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Options;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Auth.RefreshTokens;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        HttpRequest httpRequest,
        ClaimsPrincipal user,
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

        Result<GeneratedTokens> result = await service.GenerateNewTokensAsync(user.GetUserId(), request.RefreshToken, request.SessionId, cancellationToken);
        return result.AsHttpResponse();
    }

    public static async Task<IResult> HandleWebAsync(
        HttpContext http,
        [FromServices] Service service,
        [FromServices] IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken
    )
    {
        long userId = http.User.GetUserId();

        string? refreshToken = http.Request.Cookies[TokenType.RefreshToken.Name];
        if (refreshToken is null)
        {
            return Results.Unauthorized();
        }

        string? sessionId = http.Request.Cookies[TokenType.SessionId.Name];
        if (sessionId is null)
        {
            return Results.Unauthorized();
        }

        Result<GeneratedTokens> result = await service.GenerateNewTokensAsync(userId, refreshToken, sessionId, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.Error.AsHttpError();
        }

        GeneratedTokens generatedTokens = result.Value!;
        http.Response.Cookies.Append(TokenType.RefreshToken.Name, generatedTokens.RefreshToken, new CookieOptions()
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
