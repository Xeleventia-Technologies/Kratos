using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Serilog;

using Kratos.Api.Common;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Options;
using Kratos.Api.Common.Repositories;
using Kratos.Api.Database;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Startup;

public static class Assembly
{
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services)
    {
        var registires = typeof(Program).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IRegistry)));
        foreach (var registry in registires)
        {
            var instance = Activator.CreateInstance(registry) as IRegistry;
            instance?.AddServices(services);
        }

        return services;
    }

    public static WebApplication MapEndpointsFromAssembly(this WebApplication app)
    {
        var registires = typeof(Program).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IRegistry)));
        foreach (var registry in registires)
        {
            var instance = Activator.CreateInstance(registry) as IRegistry;
            instance?.MapEndpoints(app);
        }

        return app;
    }

    public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Common.Options.CookieOptions>(configuration.GetRequiredSection(Common.Options.CookieOptions.SectionName));
        services.Configure<EmailOptions>(configuration.GetRequiredSection(EmailOptions.SectionName));
        services.Configure<JwtOptions>(configuration.GetRequiredSection(JwtOptions.SectionName));
        services.Configure<OAuthOptions>(configuration.GetRequiredSection(OAuthOptions.SectionName));
        services.Configure<OptOptions>(configuration.GetRequiredSection(OptOptions.SectionName));

        return services;
    }

    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IGoogleTokenService, GoogleTokenService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<HtmxService>();

        services.AddScoped<IOtpRepository, OtpRepository>();
        services.AddScoped<IUserSessionRepository, UserSessionRepository>();

        services.AddScoped<IImageUploadService, ImageUploadService>();
        
        return services;
    }

    public static IServiceCollection AddFirebase(this IServiceCollection services)
    {
        // TODO: Add firebase
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DatabaseContext>(options => 
        {
            options.UseNpgsql(DataSource.OfPostgres(connectionString), o => o.MigrationsHistoryTable(DatabaseContext.MigrationsHistoryTableName));
        });

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
            
        return services;
    }
    
    public static ConfigureHostBuilder UseSerilogWithConfig(this ConfigureHostBuilder host, IConfiguration config)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        host.UseSerilog(logger);
        return host;
    }
}
