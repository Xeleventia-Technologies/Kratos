using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.Add;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromForm] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Adding Service with name {Name}", request.Name);
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.AddAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not add service. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Service added successfully");
        return Results.Created();
    }
}
