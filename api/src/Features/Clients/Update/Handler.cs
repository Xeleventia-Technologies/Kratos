using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Clients.Update;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long clientId,
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Updating client with ID {Id}", clientId);
        
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.UpdateAsync(clientId, request, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not update client. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }
        
        logger.LogInformation("Client updated successfully");
        return result.AsHttpResponse();
    }
}
