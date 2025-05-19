using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Options;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Utils;

namespace Kratos.Api.Features.Auth.Google;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Google login request received");

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("Google login request validation failed. Reason: {ErrorMessage}", validationResult.Errors[0].ErrorMessage);
            return validationResult.AsHttpError();
        }

        Result<GeneratedTokens> loginWithGoogleResult = await service.LoginWithGoogleAsync(request.GoogleToken, request.SessionId, cancellationToken);
        if (!loginWithGoogleResult.IsSuccess)
        {
            logger.LogError("[Auth/Google] Google login request failed. Reason: {Message}", loginWithGoogleResult.Error?.Message ?? "No message provided");
            return loginWithGoogleResult.Error.AsHttpError();
        }

        logger.LogInformation("Google login request completed successfully.");
        return loginWithGoogleResult.AsHttpResponse();
    }

    public static async Task<IResult> HandleWebAsync(
        HttpRequest httpRequest,
        HttpResponse httpResponse,
        [FromBody] RequestWeb request,
        [FromServices] IValidator<RequestWeb> requestValidator,
        [FromServices] Service service,
        [FromServices] IOptions<JwtOptions> jwtOptions,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        string? sessionId = httpRequest.Cookies[TokenType.SessionId.Name];

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogWarning("[Auth/Google] Google login request validation failed. Reason: {ErrorMessage}", validationResult.Errors[0].ErrorMessage);
            return validationResult.AsHttpError();
        }

        Result<GeneratedTokens> result = await service.LoginWithGoogleAsync(request.GoogleToken, sessionId, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("[Auth/Google] Google login request failed. Reason: {Message}", result.Error?.Message ?? "No message provided");
            return result.Error.AsHttpError();
        }

        GeneratedTokens generatedTokens = result.Value!;

        httpResponse.AppendCookie(TokenType.SessionId.Name, generatedTokens.SessionId, path: "/", DateTimeOffset.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays));
        httpResponse.AppendCookie(TokenType.RefreshToken.Name, generatedTokens.RefreshToken, Registry.RefreshTokensWebUrl, DateTimeOffset.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays));

        logger.LogInformation("[Auth/Google] Google login request completed successfully.");
     
        OnlyAccessToken onlyAccessToken = new OnlyAccessToken(generatedTokens.AccessToken);
        Result<OnlyAccessToken> onlyAccessTokenResult = result.SuccessStatus!.Value == SuccessStatus.Created
            ? Result.Created(onlyAccessToken)
            : Result.Success(onlyAccessToken);

        return onlyAccessTokenResult.AsHttpResponse();
    }
}
