using FluentValidation;

namespace Kratos.Api.Features.Auth.Logout;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.SessionId)
            .NotEmpty();
    }
}
