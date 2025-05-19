namespace Kratos.Api.Features.Articles.GetApproved;

public class Response
{
    public int TotalCount { get; set; }
    public List<Article> Articles { get; set; } = [];

    public record Article(
        long Id,
        string Title,
        string Summary,
        string CreatedByUserFullName,
        string CreatedAt
    );
}

public record FilterGetParams(
    int? Count,
    int? Page,
    string? Search,
    DateTime? From,
    DateTime? To
);

public record FilterParams(
    int Count,
    int Page,
    string? Search,
    DateTime? From,
    DateTime? To
);
