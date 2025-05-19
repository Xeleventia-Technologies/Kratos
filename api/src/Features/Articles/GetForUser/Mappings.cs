using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.GetForUser;

public static class Mappings
{
    public static FilterParams AsFilterParams(this FilterGetParams filters) => new(
        filters.Count is not null ? filters.Count.Value : 10,
        filters.Page is not null ? filters.Page.Value : 1,
        filters.Search,
        filters.Approval is null ? null : Enum.Parse<Enums.ArticleApprovalStatus>(filters.Approval, ignoreCase: true),
        filters.From,
        filters.To
    );
}
