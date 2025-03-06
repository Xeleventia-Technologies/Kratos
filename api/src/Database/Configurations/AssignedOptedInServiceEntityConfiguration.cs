using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class AssignedOptedInServiceEntityConfiguration : IEntityTypeConfiguration<AssignedOptedInService>
{
    public void Configure(EntityTypeBuilder<AssignedOptedInService> builder)
    {
        builder.ToTable("AssignedOptedInServices");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder
            .HasOne(x => x.OptedInService)
            .WithMany(x => x.AssignedTasks)
            .HasForeignKey(x => x.OptedInServiceId);

        builder
            .HasOne(x => x.AssignedToUser)
            .WithMany()
            .HasForeignKey(x => x.AssignedToUserId);

        builder
            .HasOne(x => x.AssignedByUser)
            .WithMany()
            .HasForeignKey(x => x.AssignedByUserId);
    }
}