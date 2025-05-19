using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Articles.Update;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long articleId,
        [FromForm] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Updating article with ID: {Id}", articleId);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.UpdateAsync(articleId, request, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not update article. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }
        
        logger.LogInformation("Article updated successfully");
        return result.AsHttpResponse();
    }
}
