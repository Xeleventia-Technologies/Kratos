using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Auth.SignUp;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromBody] Request request,
        [FromServices] IValidator<Request> requestValidator,
        [FromServices] Service service,
        CancellationToken cancellationToken
    )
    {
        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.AsHttpError();
        }

        Result result = await service.ValidateOtpAndSignUpAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return result.Error.AsHttpError();
        }

        return Results.Created();
    }
}
