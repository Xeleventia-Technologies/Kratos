using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Profiles.Get;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();
        logger.LogInformation("Getting profile for user {UserId}", userId);

        Result<Projections.Profile> result = await service.GetForUser(userId, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not get profile. Reason: {Error}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Profile retrieved successfully.");
        return result.AsHttpResponse();
    }
}
