using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class ForumThread
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;

    public long CreatedByUserId { get; set; }
    public virtual User CreatedByUser { get; set; } = null!;

    public virtual List<ThreadComment> Comments { get; set; } = null!;
    public virtual List<ForumThreadVote> Votes { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
