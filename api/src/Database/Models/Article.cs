using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class Article
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? ImageFileName { get; set; } = null!;

    public Enums.ArticleApprovalStatus ApprovalStatus { get; set; }

    public long CreatedByUserId { get; set; }
    public virtual User CreatedByUser { get; set; } = null!;

    public long? ApprovedByUserId { get; set; }
    public virtual User? ApprovedByUser { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
