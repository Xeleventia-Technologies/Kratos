using FluentValidation;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Auth.GenerateOtp;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Purpose)
            .NotEmpty()
            .IsEnumName(typeof(Enums.OtpPurpose), caseSensitive: false)
                .WithMessage($"Allowed values are: {Enums.CommaSeparated<Enums.OtpPurpose>()}");
    }
}
