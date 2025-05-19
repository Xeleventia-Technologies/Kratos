using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations;

public class ForumThreadCommentEntityConfiguration : IEntityTypeConfiguration<ForumThreadComment>
{
    public void Configure(EntityTypeBuilder<ForumThreadComment> builder)
    {
        builder.ToTable("forum_thread_comments");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityAlwaysColumn();

        builder.Property(x => x.Text)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.ForumThreadId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder
            .HasOne(x => x.ForumThread)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ForumThreadId);

        builder
            .HasOne(x => x.ParentComment)
            .WithMany(x => x.ChildComments)
            .HasForeignKey(x => x.ParentCommentId);

        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasIndex(x => x.ForumThreadId);
        builder.HasIndex(x => x.IsDeleted);
    }
}
