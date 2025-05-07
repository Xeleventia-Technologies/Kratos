using FluentValidation;

namespace Kratos.Api.Features.Auth.Login;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
         RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}

public class RequestWebValidator : AbstractValidator<RequestWeb>
{
    public RequestWebValidator()
    {
         RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
