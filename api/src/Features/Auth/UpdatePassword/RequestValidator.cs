using FluentValidation;

namespace Kratos.Api.Features.Auth.UpdatePassword;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
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
    }
}
