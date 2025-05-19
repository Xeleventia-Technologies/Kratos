using FluentValidation;

namespace Kratos.Api.Features.Feeback.Add;

public class RequestValidator : AbstractValidator<Request>
{
    private static readonly string MustBeBetweenOneAndFive = "Rating must be between 1 and 5";

    public RequestValidator()
    {
        RuleFor(x => x.OutOfFiveRating)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
                .WithMessage(MustBeBetweenOneAndFive)
            .LessThanOrEqualTo(5)
                .WithMessage(MustBeBetweenOneAndFive);

        RuleFor(x => x.Comment)
            .NotEmpty();
    }
}
