using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Articles.GetForUser;

public class Handler
{
    public static async Task<IResult> HandelAsync(
        ClaimsPrincipal user,
        [AsParameters] FilterGetParams filters,
        [FromServices] IValidator<FilterGetParams> filtersValidator,
        [FromServices] Service service,
        CancellationToken cancellationToken
    )
    {
        long userId = user.GetUserId();

        ValidationResult validationResult = await filtersValidator.ValidateAsync(filters, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.AsHttpError();
        }

        Result<Response> result = await service.GetForUserAsync(userId, filters, cancellationToken);
        return result.AsHttpResponse();
    }
}
