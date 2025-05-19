using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Auth.LogoutAll;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromServices] Service service,
        ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();
        logger.LogInformation("Logging out all sessions of user {UserId}", userId);

        Result result = await service.LogoutAllAsync(userId, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not logout. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("All sessions logged out successfully");
        return result.AsHttpResponse();
    }

    public static async Task<IResult> HandleWebAsync(
        ClaimsPrincipal user,
        [FromServices] Service service,
        ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();
        logger.LogInformation("Logging out all sessions of user {UserId}", userId);

        Result result = await service.LogoutAllAsync(userId, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not logout. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("All sessions logged out successfully");
        return result.AsHttpResponse();
    }
}
