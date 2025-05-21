using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class CommunityThreadEntityConfiguration : IEntityTypeConfiguration<CommunityThread>
{
    public void Configure(EntityTypeBuilder<CommunityThread> builder)
    {
        builder.ToTable("community_threads");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.CommunityId)
            .IsRequired();

        builder.Property(x => x.CreatedByUserId)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder
            .HasOne(x => x.Community)
            .WithMany(x => x.Threads)
            .HasForeignKey(x => x.CommunityId);

        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId);

        builder.HasIndex(x => x.CommunityId);
        builder.HasIndex(x => x.CreatedByUserId);
        builder.HasIndex(x => x.CreatedAt);
    }
}
