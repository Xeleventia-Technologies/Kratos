using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.Get;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [AsParameters] GetParams getParams,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Getting services");
        Result<List<Projections.Service>> serviceResult = await service.GetAsync(getParams, cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not get services. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Services retrieved successfully.");
        return serviceResult.AsHttpResponse();
    }
}
