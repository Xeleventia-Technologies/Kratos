using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using System.Security.Claims;

namespace Kratos.Api.Features.Testimonials.Add;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();
        logger.LogInformation("Adding testimonial for user {UserId} with text '{Text}'", userId, request.Text);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogError("Request validation failed. Reason: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result serviceResult = await service.AddAsync(userId, request.AsTestimonial(), cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogError("Could not add testimonial. Reason: {Error}", serviceResult.Error!.Message);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Testimonial added successfully.");
        return Results.Created();
    }
}
