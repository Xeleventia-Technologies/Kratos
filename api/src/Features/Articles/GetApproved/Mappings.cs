using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.GetApproved;

public static class Mappings
{
    public static FilterParams AsFilterParams(this FilterGetParams filters) => new(
        filters.Count is not null ? filters.Count.Value : 10,
        filters.Page is not null ? filters.Page.Value : 1,
        filters.Search,
        filters.From,
        filters.To
    );
}
