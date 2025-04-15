using FluentValidation;

namespace Kratos.Api.Features.Auth.RefreshTokens;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotEmpty();

        RuleFor(x => x.RefreshToken)
            .NotEmpty();

        RuleFor(x => x.SessionId)
            .NotEmpty();
    }
}
