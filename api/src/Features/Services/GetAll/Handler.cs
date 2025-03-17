using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.GetAll;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Getting all services");
        Result<List<Projections.Service>> serviceResult = await service.GetAllAsync(cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not get services. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Services retrieved successfully.");
        return serviceResult.AsHttpResponse();
    }
}
