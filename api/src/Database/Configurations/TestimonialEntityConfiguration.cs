using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class TestimonialEntityConfiguration : IEntityTypeConfiguration<Testimonial>
{
    public void Configure(EntityTypeBuilder<Testimonial> builder)
    {
        builder.ToTable("testimonials");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Text)
            .IsRequired()
            .HasColumnType("text");

        builder
            .HasOne(x => x.User)
            .WithOne()
            .HasForeignKey<Testimonial>(x => x.UserId);
    }
}
