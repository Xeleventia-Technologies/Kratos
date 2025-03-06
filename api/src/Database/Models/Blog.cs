using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class Blog
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? ImageFileName { get; set; } = null!;

    public Enums.BlogApprovalStaus ApprovalStatus { get; set; }

    public long CreatedByUserId { get; set; }
    public virtual User CreatedByUser { get; set; } = null!;

    public long? ApprovedByUserId { get; set; }
    public virtual User? ApprovedByUser { get; set; }

    public List<BlogVote> Votes { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
