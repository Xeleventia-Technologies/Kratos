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

    public DbSet<Article> Articles { get; set; }
    public DbSet<AssignedOptedInService> AssignedOptedInServices { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Community> Communitys { get; set; }
    public DbSet<CommunityThread> CommunityThreads { get; set; }
    public DbSet<CommunityThreadComment> CommunityThreadComments { get; set; }
    public DbSet<CommunityThreadImage> CommunityThreadImages { get; set; }
    public DbSet<CommunityThreadVote> CommunityThreadVotes { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<OptedInService> OptedInServices { get; set; }
    public DbSet<OptedInServiceQuery> OptedInServiceQueries { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Service> Services { get; set; }
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
        builder.HasPostgresEnum<Enums.ArticleApprovalStatus>();
        builder.HasPostgresEnum<Enums.CommunityThreadVoteType>();
        builder.HasPostgresEnum<Enums.AssignedOptedInServiceStatus>();

        new UserOtpEntityConfiguration().Configure(builder.Entity<UserOtp>());
        new UserSessionEntityConfiguration().Configure(builder.Entity<UserSession>());

        new ArticleEntityConfiguration().Configure(builder.Entity<Article>());
        new AssignedOptedInServiceEntityConfiguration().Configure(builder.Entity<AssignedOptedInService>());
        new ClientEntityConfiguration().Configure(builder.Entity<Client>());
        new CommunityEntityConfiguration().Configure(builder.Entity<Community>());
        new CommunityThreadCommentEntityConfiguration().Configure(builder.Entity<CommunityThreadComment>());
        new CommunityThreadEntityConfiguration().Configure(builder.Entity<CommunityThread>());
        new CommunityThreadImageEntityConfiguration().Configure(builder.Entity<CommunityThreadImage>());
        new CommunityThreadVoteEntityConfiguration().Configure(builder.Entity<CommunityThreadVote>());
        new FeedbackEntityConfiguration().Configure(builder.Entity<Feedback>());
        new MemberEntityConfiguration().Configure(builder.Entity<Member>());
        new OptedInServiceEntityConfiguration().Configure(builder.Entity<OptedInService>());
        new OptedInServiceQueryEntityConfiguration().Configure(builder.Entity<OptedInServiceQuery>());
        new ProfileEntityConfiguration().Configure(builder.Entity<Profile>());
        new ServiceEntityConfiguration().Configure(builder.Entity<Service>());
        new TestimonialEntityConfiguration().Configure(builder.Entity<Testimonial>());
    }
}
