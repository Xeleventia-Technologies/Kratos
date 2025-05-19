using FluentValidation;

namespace Kratos.Api.Features.Articles.GetApproved;

public class ParamsValidator : AbstractValidator<FilterGetParams>
{
    public ParamsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);
            
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.From)
            .LessThanOrEqualTo(x => x.To)
            .When(x => x.From is not null && x.To is not null);
    }
}
