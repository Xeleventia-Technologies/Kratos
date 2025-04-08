using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database.Models;
using Kratos.Api.Database.Configurations;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database;

public class DatabaseContext(DbContextOptions options) : IdentityDbContext<User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    public DbSet<AssignedOptedInService> AssignedOptedInServices { get; set; }
    public DbSet<AssignedOptedInServiceQuery> AssignedOptedInServiceQueries { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogVote> BlogVotes { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<ForumThread> ForumThreads { get; set; }
    public DbSet<ForumThreadVote> ForumThreadVotes { get; set; }
    public DbSet<OptedInService> OptedInServices { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ThreadComment> ThreadComments { get; set; }
    public DbSet<Testimonial> Testimonials { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresEnum<Enums.BlogApprovalStaus>();
        builder.HasPostgresEnum<Enums.BlogVoteType>();
        builder.HasPostgresEnum<Enums.ForumThreadVoteType>();

        new AssignedOptedInServiceEntityConfiguration().Configure(builder.Entity<AssignedOptedInService>());
        new AssignedOptedInServiceQueryEntityConfiguration().Configure(builder.Entity<AssignedOptedInServiceQuery>());
        new BlogEntityConfiguration().Configure(builder.Entity<Blog>());
        new BlogVoteEntityConfiguration().Configure(builder.Entity<BlogVote>());
        new ClientEntityConfiguration().Configure(builder.Entity<Client>());
        new FeedbackEntityConfiguration().Configure(builder.Entity<Feedback>());
        new ForumEntityConfiguration().Configure(builder.Entity<Forum>());
        new ForumThreadEntityConfiguration().Configure(builder.Entity<ForumThread>());
        new ForumThreadVoteEntityConfiguration().Configure(builder.Entity<ForumThreadVote>());
        new OptedInServiceEntityConfiguration().Configure(builder.Entity<OptedInService>());
        new ProfileEntityConfiguration().Configure(builder.Entity<Profile>());
        new ServiceEntityConfiguration().Configure(builder.Entity<Service>());
        new ThreadCommentEntityConfiguration().Configure(builder.Entity<ThreadComment>());
        new TestimonialEntityConfiguration().Configure(builder.Entity<Testimonial>());
    }
}
