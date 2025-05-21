using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class CommunityThreadVoteEntityConfiguration : IEntityTypeConfiguration<CommunityThreadVote>
{
    public void Configure(EntityTypeBuilder<CommunityThreadVote> builder)
    {
        builder.ToTable("community_thread_votes");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.VoteType)
            .IsRequired();

        builder.Property(x => x.VotedByUserId)
            .IsRequired();

        builder.Property(x => x.ThreadId)
            .IsRequired();

        builder
            .HasOne(x => x.VotedByUser)
            .WithMany()
            .HasForeignKey(x => x.VotedByUserId);

        builder
            .HasOne(x => x.Thread)
            .WithMany(x => x.Votes)
            .HasForeignKey(x => x.ThreadId);

        builder.HasIndex(x => x.ThreadId);

        builder
            .HasIndex(x => new { x.ThreadId, x.VotedByUserId })
            .IsUnique();
    }
}
