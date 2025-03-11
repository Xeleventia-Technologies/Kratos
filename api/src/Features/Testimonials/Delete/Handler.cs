using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Testimonials.Delete;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long userId,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Deleteing testimonial for user {UserId}", userId);
        Result serviceResult = await service.DeleteTestimonialForUserAsync(userId, cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not delete testimonial. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Testimonial for user {UserId} deleted successfully.", userId);
        return serviceResult.AsHttpResponse();
    }
}
