using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Auth.UpdatePassword;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromBody] Request request,
        [FromServices] RequestValidator requestValidator,
        [FromServices] Service service,
        ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();
        logger.LogInformation("Updating password for User ID '{UserId}'", userId);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.UpdatePasswordAsync(userId, request);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not update password. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Password updated successfully");
        return result.AsHttpResponse();
    }
}
