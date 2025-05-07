using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class BlogEntityConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("blogs");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Summary)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Text)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.ImageFileName)
            .HasMaxLength(255);

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
            .HasOne(x => x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.ApprovedByUser)
            .WithMany()
            .HasForeignKey(x => x.ApprovedByUserId);

        builder.HasIndex(x => x.ApprovalStatus);
        builder.HasIndex(x => x.CreatedByUserId);
        builder.HasIndex(x => x.ApprovedByUserId);
        builder.HasIndex(x => x.IsDeleted);
        builder.HasIndex(x => x.CreatedAt);
    }
}
