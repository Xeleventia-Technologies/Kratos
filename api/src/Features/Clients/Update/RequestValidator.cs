using FluentValidation;

namespace Kratos.Api.Features.Clients.Update;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.CloudStorageLink)
            .NotEmpty();
    }
}