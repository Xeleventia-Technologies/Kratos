using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class FeedbackEntityConfiguration : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("feedbacks");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.OutOfFiveRating)
            .IsRequired();

        builder.Property(x => x.Comment)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder
            .HasIndex(x => x.UserId)
            .IsUnique();
    }
}
