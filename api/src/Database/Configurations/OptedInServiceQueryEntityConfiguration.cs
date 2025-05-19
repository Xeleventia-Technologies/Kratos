using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class OptedInServiceQueryEntityConfiguration : IEntityTypeConfiguration<OptedInServiceQuery>
{
    public void Configure(EntityTypeBuilder<OptedInServiceQuery> builder)
    {
        builder.ToTable("opted_in_service_queries");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Question)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Answer)
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
            .HasForeignKey<OptedInServiceQuery>(x => x.AnsweredByUserId);

        builder
            .HasOne(x => x.OptedInService)
            .WithMany(x => x.Queries)
            .HasForeignKey(x => x.OptedInServiceId);
    }
}
