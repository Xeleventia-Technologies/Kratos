using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Testimonials.GetAll;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Getting all testimonials");
        Result<List<Projections.Testimonial>> serviceResult = await service.GetAllTestimonialsAsync(cancellationToken);

        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not get testimonials. Reason: {Reason}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Testimonials retrieved successfully.");
        return serviceResult.AsHttpResponse();
    }   
}
