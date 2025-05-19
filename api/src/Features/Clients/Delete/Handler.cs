using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Clients.Delete;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long clientId,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Deleting client with ID: {Id}", clientId);

        Result serviceResult = await service.DeleteAsync(clientId, cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not delete client. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Client deleted successfully.");
        return serviceResult.AsHttpResponse();
    }
}
