using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Feeback.GetAll;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Getting all feedbacks");
        Result<List<Projections.Feedback>> serviceResult = await service.GetAllAsync(cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not get feedbacks. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Feedbacks retrieved successfully.");
        return serviceResult.AsHttpResponse();
    }
}
