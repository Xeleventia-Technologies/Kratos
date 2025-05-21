using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class CommunityThreadVote
{
    public long Id { get; set; }

    public Enums.CommunityThreadVoteType VoteType { get; set; }

    public long VotedByUserId { get; set; }
    public virtual User VotedByUser { get; set; } = null!;

    public long ThreadId { get; set; }
    public virtual CommunityThread Thread { get; set; } = null!;
}
