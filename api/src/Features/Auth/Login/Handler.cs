using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Options;
using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Utils;

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

        Result<GeneratedTokens> loginResult = await service.LoginAsync(request.Email, request.Password, request.SessionId, cancellationToken);
        return loginResult.AsHttpResponse();
    }

    public static async Task<IResult> HandleWebAsync(
        HttpRequest httpRequest,
        HttpResponse httpResponse,
        [FromBody] RequestWeb request,
        [FromServices] IValidator<RequestWeb> requestValidator,
        [FromServices] Service service,
        [FromServices] IOptions<JwtOptions> jwtOptions,
        CancellationToken cancellationToken
    )
    {
        string? sessionId = httpRequest.Cookies[TokenType.SessionId.Name];

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.AsHttpError();
        }

        Result<GeneratedTokens> result = await service.LoginAsync(request.Email, request.Password, sessionId, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.Error.AsHttpError();
        }

        GeneratedTokens generatedTokens = result.Value!;

        httpResponse.AppendCookie(TokenType.SessionId.Name, generatedTokens.SessionId, path: "/", DateTimeOffset.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays));
        httpResponse.AppendCookie(TokenType.RefreshToken.Name, generatedTokens.RefreshToken, Registry.RefreshTokensWebUrl, DateTimeOffset.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays));
        
        return Results.Ok(new OnlyAccessToken(generatedTokens.AccessToken));
    }
}
