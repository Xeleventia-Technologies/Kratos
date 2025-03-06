using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class OptedInServiceEntityConfiguration : IEntityTypeConfiguration<OptedInService>
{
    public void Configure(EntityTypeBuilder<OptedInService> builder)
    {
        builder.ToTable("OptedInServices");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder
            .HasOne(x => x.Client)
            .WithOne()
            .HasForeignKey<OptedInService>(x => x.ClientId);

        builder
            .HasOne(x => x.Service)
            .WithOne()
            .HasForeignKey<OptedInService>(x => x.ServiceId);
    }
}