using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class BlogVote
{
    public long Id { get; set; }

    public Enums.BlogVoteType VoteType { get; set; }

    public long BlogId { get; set; }
    public Blog Blog { get; set; } = null!;

    public long VotedByUserId { get; set; }
    public User VotedByUser { get; set; } = null!;
}
