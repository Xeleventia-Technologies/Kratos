using FluentValidation;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.ChangeApprovalStatus;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.ApprovalStatus)
            .NotEmpty()
            .IsEnumName(typeof(Enums.ArticleApprovalStatus), caseSensitive: false);
    }
}
