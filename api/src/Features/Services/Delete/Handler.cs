using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.Delete;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long serviceId,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Deleting service with ID: {Id}", serviceId);
        Result serviceResult = await service.DeleteAsync(serviceId, cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not delete service. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Service deleted successfully.");
        return serviceResult.AsHttpResponse();
    }
}
