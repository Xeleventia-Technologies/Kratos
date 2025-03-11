using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Clients.Add;

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
        logger.LogInformation("Adding Client for user {UserId} with cloud storage link {CloudStorageLink}", request.UserId, request.CloudStorageLink);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("RequestValidation failed");
            return validationResult.AsHttpError();
        }

        Result serviceResult = await service.AddAsync(request.AsClient(), cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogInformation("Operation was not successful: {Error}", serviceResult.Error);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Client added successfully");
        return Results.Created();
    }
}