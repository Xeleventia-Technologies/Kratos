using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Services.Update;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long serviceId,
        [FromForm] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Updating service with ID {Id}", serviceId);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.UpdateAsync(serviceId, request.AsService(), request.Image, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not update service. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Service updated successfully");
        return result.AsHttpResponse();
    }
}
