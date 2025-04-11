using FluentValidation;

namespace Kratos.Api.Features.Auth.SignUp;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters")
            .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"[0-9]")
                .WithMessage("Password must contain at least one number")
            .Matches(@"[^\w\s]")
                .WithMessage("Password must contain at least one special character");

        RuleFor(x => x.Otp)
            .NotEmpty()
            .Length(6)
                .WithMessage("OTP must be 6 digits")
            .Matches(@"^\d+$")
                .WithMessage("OTP must be a number");
    }
}
