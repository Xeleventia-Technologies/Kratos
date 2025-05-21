using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using System.Security.Claims;

namespace Kratos.Api.Features.Articles.GetById;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromRoute] long articleId,
        [FromServices] Service service,
        [FromServices] ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        string? userRole = user.GetUserRole();
        logger.LogInformation("Retrieving article with ID: {Id}", articleId);

        Result<Article> result = await service.GetByIdAsync(userRole, articleId, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not retrieve article. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Article with ID: {Id} retrieved successfully", articleId);
        return result.AsHttpResponse();
    }
}
