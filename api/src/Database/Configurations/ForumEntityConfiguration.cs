namespace Kratos.Api.Database.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

public class ForumEntityConfiguration : IEntityTypeConfiguration<Forum>
{
    public void Configure(EntityTypeBuilder<Forum> builder)
    {
        builder.ToTable("Forums");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(x => x.Summary)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ImageFileName)
            .IsRequired()
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

        builder.HasIndex(x => x.CreatedByUserId);
        builder.HasIndex(x => x.IsDeleted);
    }
}
