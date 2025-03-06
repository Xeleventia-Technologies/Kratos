using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class ForumThreadVoteEntityConfiguration : IEntityTypeConfiguration<ForumThreadVote>
{
    public void Configure(EntityTypeBuilder<ForumThreadVote> builder)
    {
        builder.ToTable("ForumThreadVotes");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.VoteType)
            .IsRequired();

        builder.Property(x => x.VotedByUserId)
            .IsRequired();

        builder.Property(x => x.ForumThreadId)
            .IsRequired();

        builder
            .HasOne(x => x.VotedByUser)
            .WithMany()
            .HasForeignKey(x => x.VotedByUserId);

        builder
            .HasOne(x => x.ForumThread)
            .WithMany(x => x.Votes)
            .HasForeignKey(x => x.ForumThreadId);

        builder.HasIndex(x => x.ForumThreadId);

        builder
            .HasIndex(x => new { x.ForumThreadId, x.VotedByUserId })
            .IsUnique();
    }
}
