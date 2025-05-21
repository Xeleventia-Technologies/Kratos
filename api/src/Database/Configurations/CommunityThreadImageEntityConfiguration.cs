using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class CommunityThreadImageEntityConfiguration : IEntityTypeConfiguration<CommunityThreadImage>
{
    public void Configure(EntityTypeBuilder<CommunityThreadImage> builder)
    {
        builder.ToTable("community_thread_images");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.ImageFileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ThreadId)
            .IsRequired();

        builder
            .HasOne(x => x.Thread)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ThreadId);
    }
}
