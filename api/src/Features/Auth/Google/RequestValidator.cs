using FluentValidation;

namespace Kratos.Api.Features.Auth.Google;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.GoogleToken)
            .NotEmpty();
    }
}
