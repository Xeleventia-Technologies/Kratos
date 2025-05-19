using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Members.Add;

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
        logger.LogInformation("Adding Member with name {FullName}", request.FullName);
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result serviceResult = await service.AddAsync(request.AsMember(), request.DisplayPicture, cancellationToken);
        if (!serviceResult.IsSuccess)
        {
            logger.LogInformation("Operation was not successful: {Error}", serviceResult.Error);
            return serviceResult.Error.AsHttpError();
        }

        logger.LogInformation("Member added successfully");
        return Results.Created();        
    }
}
