using Microsoft.AspNetCore.Mvc;

using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Auth.Logout;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromBody] Request request,
        [FromServices] RequestValidator requestValidator,
        [FromServices] Service service,
        ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Logging out session {SessionId}", request.SessionId);
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.LogoutAsync(request.SessionId, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not logout. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Session logged out successfully");
        return result.AsHttpResponse();
    }

    public static async Task<IResult> HandleWebAsync(
        HttpRequest httpRequest,
        [FromServices] Service service,
        ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        string? sessionId = httpRequest.Cookies[TokenType.SessionId.Name];
        if (sessionId is null)
        {
            return Results.Unauthorized();
        }

        logger.LogInformation("Logging out session {SessionId}", sessionId);

        Result result = await service.LogoutAsync(sessionId, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not logout. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Session logged out successfully");
        return result.AsHttpResponse();
    }
}
