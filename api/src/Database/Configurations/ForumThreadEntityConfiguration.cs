namespace Kratos.Api.Database.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

public class ForumThreadEntityConfiguration : IEntityTypeConfiguration<ForumThread>
{
    public void Configure(EntityTypeBuilder<ForumThread> builder)
    {
        builder.ToTable("ForumThreads");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ForumId)
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
            .HasOne(x => x.Forum)
            .WithMany(x => x.Threads)
            .HasForeignKey(x => x.ForumId);

        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId);

        builder.HasIndex(x => x.ForumId);
        builder.HasIndex(x => x.CreatedByUserId);
        builder.HasIndex(x => x.CreatedAt);
    }
}
