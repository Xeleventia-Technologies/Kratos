using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class ThreadComment
{
    public long Id { get; set; }

    public string Text { get; set; } = null!;

    public long ForumThreadId { get; set; }
    public virtual ForumThread ForumThread { get; set; } = null!;

    public long? ParentCommentId { get; set; }
    public virtual ThreadComment? ParentComment { get; set; } = null!;

    public long UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public virtual List<ThreadComment> ChildComments { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
