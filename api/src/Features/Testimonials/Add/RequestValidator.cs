using FluentValidation;

namespace Kratos.Api.Features.Testimonials.Add;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty();
    }
}
