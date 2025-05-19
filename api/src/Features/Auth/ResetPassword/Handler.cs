using Microsoft.AspNetCore.Mvc;

using FluentValidation.Results;

using Kratos.Api.Common.Extensions;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Auth.ResetPassword;

public class Handler
{
    public static async Task<IResult> HandleAsync(
        [FromBody] Request request,
        [FromServices] RequestValidator requestValidator,
        [FromServices] Service service,
        ILogger<Handler> logger,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Resetting password for User with email '{Email}'", request.Email);

        ValidationResult validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("Request validation failed: {Error}", validationResult.ToString());
            return validationResult.AsHttpError();
        }

        Result result = await service.ResetPasswordAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            logger.LogError("Could not reset password. Reason: {Reason}", result.Error!.Message);
            return result.Error.AsHttpError();
        }

        logger.LogInformation("Password reset successfully");
        return result.AsHttpResponse();
    }
}
