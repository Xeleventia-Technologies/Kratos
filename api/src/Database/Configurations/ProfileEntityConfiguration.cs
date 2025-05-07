using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class ProfileEntityConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("profiles");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.DisplayPictureFileName)
            .HasMaxLength(255);

        builder.Property(x => x.Bio)
            .HasColumnType("text");

        builder.Property(x => x.MobileNumber)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Profile)
            .HasForeignKey<Profile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId).IsUnique();
    }
}
