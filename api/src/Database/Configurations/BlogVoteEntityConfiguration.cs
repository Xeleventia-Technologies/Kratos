namespace Kratos.Api.Database.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

public class BlogVoteEntityConfiguration : IEntityTypeConfiguration<BlogVote>
{
    public void Configure(EntityTypeBuilder<BlogVote> builder)
    {
        builder.ToTable("BlogVotes");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.BlogId, x.VotedByUserId }).IsUnique();
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.VoteType)
            .IsRequired();

        builder.Property(x => x.BlogId)
            .IsRequired();

        builder.Property(x => x.VotedByUserId)
            .IsRequired();

        builder
            .HasOne(x => x.Blog)
            .WithMany(x => x.Votes)
            .HasForeignKey(x => x.BlogId);

        builder.HasIndex(x => x.BlogId);
        
        builder
            .HasIndex(x => new { x.BlogId, x.VotedByUserId })
            .IsUnique();
    }
}
