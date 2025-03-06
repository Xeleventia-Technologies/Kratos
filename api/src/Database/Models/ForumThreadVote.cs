using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class ForumThreadVote
{
    public long Id { get; set; }

    public Enums.ForumThreadVoteType VoteType { get; set; }

    public long VotedByUserId { get; set; }
    public virtual User VotedByUser { get; set; } = null!;

    public long ForumThreadId { get; set; }
    public virtual ForumThread ForumThread { get; set; } = null!;
}
