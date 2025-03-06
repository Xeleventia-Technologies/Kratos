using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Testimonials.Add;

public class Handler
{
    public static async Task<IResult> AddAsync(
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Adding testimonial for user {UserId} with text {Text}", request.UserId, request.Text);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("RequestValidation failed");
            return validationResult.AsHttpError();
        }

        Result serviceResult = await service.AddAsync(request.AsTestimonial(), cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogInformation("Operation was not successful: {Error}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Operation was successful");
        return Results.Created();
    }
}
