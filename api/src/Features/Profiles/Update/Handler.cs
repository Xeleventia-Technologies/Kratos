using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Profiles.Update;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromForm] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();
        logger.LogInformation("Updating profile for user {UserId}", userId);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result serviceResult = await service.UpdateAsync(userId, request, cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not update profile. Reason: {Error}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Profile updated successfully.");
        return Results.Ok();
    }
}
