using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Members.Delete;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long memberId,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Deleting member with ID: {Id}", memberId);

        Result serviceResult = await service.DeleteAsync(memberId, cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not delete member. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Member deleted successfully.");
        return serviceResult.AsHttpResponse();
    }
}
