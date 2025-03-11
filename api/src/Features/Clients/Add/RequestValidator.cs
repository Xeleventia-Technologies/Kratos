using FluentValidation;

namespace Kratos.Api.Features.Clients.Add;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.CloudStorageLink)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty()
            .GreaterThan(0);
    }
}