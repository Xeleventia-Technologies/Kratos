using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.GetById;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long serviceId,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Getting service with ID: {Id}", serviceId);
        Result<Projections.Service> serviceResult = await service.GetByIdAsync(serviceId, cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not get service. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Service retrieved successfully.");
        return serviceResult.AsHttpResponse();
    }
}
