using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class ServiceEntityConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("services");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Summary)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.ImageFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.SeoFriendlyName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.SeoFriendlyName).IsUnique();
        builder.HasIndex(x => x.IsDeleted);

        builder
            .HasOne(x => x.ParentService)
            .WithMany(x => x.ChildServices)
            .HasForeignKey(x => x.ParentServiceId);
    }
}
