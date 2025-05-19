using FluentValidation;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.GetForUser;

public class ParamsValidator : AbstractValidator<FilterGetParams>
{
    public ParamsValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);
            
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Approval)
            .IsEnumName(typeof(Enums.ArticleApprovalStatus), caseSensitive: false);

        RuleFor(x => x.From)
            .LessThanOrEqualTo(x => x.To)
            .When(x => x.From is not null && x.To is not null);
    }
}
