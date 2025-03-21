using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Clients.GetById;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long clientId,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Getting client with ID: {Id}", clientId);
        Result<Projections.Client> serviceResult = await service.GetByIdAsync(clientId, cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not get client. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Client retrieved successfully.");
        return serviceResult.AsHttpResponse();
    }
}