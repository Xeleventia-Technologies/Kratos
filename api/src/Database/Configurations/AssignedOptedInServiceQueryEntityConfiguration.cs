using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class AssignedOptedInServiceQueryEntityConfiguration : IEntityTypeConfiguration<AssignedOptedInServiceQuery>
{
    public void Configure(EntityTypeBuilder<AssignedOptedInServiceQuery> builder)
    {
        builder.ToTable("AssignedOptedInServiceQueries");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Question)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Answer)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder
            .HasOne(x => x.AnsweredByUser)
            .WithOne()
            .HasForeignKey<AssignedOptedInServiceQuery>(x => x.AnsweredByUserId);

        builder
            .HasOne(x => x.AssignedOptedInService)
            .WithMany(x => x.AskedQueries)
            .HasForeignKey(x => x.AssignedOptedInServiceId);
    }
}