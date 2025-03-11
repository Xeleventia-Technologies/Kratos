using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Services.Add;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromForm] Request request,
        [FromServices] IValidator<Request> requestrValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Adding Service with name {Name}", request.Name);
        ValidationResult validationResult = await requestrValidator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        await service.AddAsync(request.AsService(), request.Image, cancellationToken);

        logger.LogInformation("Service added successfully");
        return Results.Created();
    }
}
