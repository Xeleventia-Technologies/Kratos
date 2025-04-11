using Microsoft.AspNetCore.Mvc;

using FluentValidation;
using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Auth.GenerateOtp;

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

        Result result = await service.GenerateAndSendOptAsync(request.Email, Enums.OtpPurpose.SignUp, cancellationToken);
        return result.AsHttpResponse();
    }
}
