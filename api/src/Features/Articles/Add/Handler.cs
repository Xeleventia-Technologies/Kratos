using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Articles.Add;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromForm] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();
        logger.LogInformation("Adding article for user {UserId}", userId);
        
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.AddAsync(userId, request, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not add article. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }
        
        logger.LogInformation("Article added successfully");
        return result.AsHttpResponse();
    }
}
