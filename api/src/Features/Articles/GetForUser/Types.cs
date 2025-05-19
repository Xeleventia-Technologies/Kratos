using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.GetForUser;

public class Response
{
    public int TotalCount { get; set; }
    public List<Article> Articles { get; set; } = [];

    public record Article(
        long Id,
        string Title,
        string Summary,
        string ApprovalStatus,
        string CreatedAt
    );
}

public record FilterGetParams(
    int? Count,
    int? Page,
    string? Search,
    string? Approval,
    DateTime? From,
    DateTime? To
);

public record FilterParams(
    int Count,
    int Page,
    string? Search,
    Enums.ArticleApprovalStatus? Approval,
    DateTime? From,
    DateTime? To
);
