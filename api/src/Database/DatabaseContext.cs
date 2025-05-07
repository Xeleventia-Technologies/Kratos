using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Kratos.Api.Database.Configurations;
using Kratos.Api.Database.Configurations.Identity;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database;

public class DatabaseContext(DbContextOptions options) : IdentityDbContext<User, Role, long, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    public const string MigrationsHistoryTableName = "_ef_migrations_history";

    public DbSet<UserOtp> UserOtps { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
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

        builder.Entity<User>().ToTable("users");
        builder.Entity<Role>().ToTable("roles");
        builder.Entity<UserClaim>().ToTable("user_claims");
        builder.Entity<UserRole>().ToTable("user_roles");
        builder.Entity<RoleClaim>().ToTable("role_claims");

        builder.Ignore<UserLogin>();
        builder.Ignore<UserToken>();

        builder.HasPostgresEnum<Enums.OtpPurpose>();
        builder.HasPostgresEnum<Enums.LoggedInWith>();
        builder.HasPostgresEnum<Enums.BlogApprovalStaus>();
        builder.HasPostgresEnum<Enums.BlogVoteType>();
        builder.HasPostgresEnum<Enums.ForumThreadVoteType>();

        new UserOtpEntityConfiguration().Configure(builder.Entity<UserOtp>());
        new UserSessionEntityConfiguration().Configure(builder.Entity<UserSession>());

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
