using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class CommunityThread
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;

    public long CommunityId { get; set; }
    public virtual Community Community { get; set; } = null!;

    public long CreatedByUserId { get; set; }
    public virtual User CreatedByUser { get; set; } = null!;

    public virtual List<CommunityThreadImage> Images { get; set; } = null!;
    public virtual List<CommunityThreadComment> Comments { get; set; } = null!;
    public virtual List<CommunityThreadVote> Votes { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
