using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Articles.ChangeApprovalStatus;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromRoute] long articleId,
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Changing approval status for article with ID: {Id}", articleId);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.ChangeApprovalStatusAsync(articleId, request.ApprovalStatus, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not change approval status. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }
        
        logger.LogInformation("Approval status changed successfully");
        return result.AsHttpResponse();
    }
}
