using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class CommunityThreadComment
{
    public long Id { get; set; }

    public string Text { get; set; } = null!;

    public long ThreadId { get; set; }
    public virtual CommunityThread Thread { get; set; } = null!;

    public long? ParentCommentId { get; set; }
    public virtual CommunityThreadComment? ParentComment { get; set; } = null!;

    public long UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public virtual List<CommunityThreadComment> ChildComments { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
