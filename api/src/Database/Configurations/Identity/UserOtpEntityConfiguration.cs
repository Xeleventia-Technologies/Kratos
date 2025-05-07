using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kratos.Api.Database.Configurations.Identity;

public class UserOtpEntityConfiguration : IEntityTypeConfiguration<UserOtp>
{
    public void Configure(EntityTypeBuilder<UserOtp> builder)
    {
        builder.ToTable("user_otps");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Otp)
            .IsRequired()
            .HasMaxLength(6)
            .IsFixedLength();

        builder.Property(x => x.GeneratedAt)
            .IsRequired();

        builder.Property(x => x.ExpiresAt)
            .IsRequired();

        builder.HasIndex(x => new { x.Email, x.Purpose });
    }
}
