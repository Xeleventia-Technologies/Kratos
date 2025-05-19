using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Members.Update;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long memberId,
        [FromForm] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Updating member with ID: {Id}", memberId);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result serviceResult = await service.UpdateAsync(memberId, request, cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogInformation("Operation was not successful: {Error}", serviceResult.Error);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Member updated successfully");
        return serviceResult.AsHttpResponse();
    }
}
