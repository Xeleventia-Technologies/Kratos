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
        logger.LogInformation("[Clients] Adding a new client with name {Name}", request.FullName);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("[Clients] RequestValidation failed. Reason: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result serviceResult = await service.AddAsync(request, cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogInformation("[Clients] Operation was not successful: {Error}", serviceResult.Error);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("[Clients] Client added successfully");
        return Results.Created();
    }
}
