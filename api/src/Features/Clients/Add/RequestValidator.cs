using FluentValidation;

namespace Kratos.Api.Features.Clients.Add;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .MinimumLength(10)
                .WithMessage("Mobile number must be at least 10 digits")
            .MaximumLength(10)
                .WithMessage("Mobile number must be at most 10 digits")
            .Matches(@"^\d+$")
                .WithMessage("Mobile number must be a number");
        
        RuleFor(x => x.CloudStorageLink)
            .NotEmpty();
    }
}
