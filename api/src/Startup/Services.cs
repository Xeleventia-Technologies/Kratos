using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Serilog;

using Kratos.Api.Common;
using Kratos.Api.Database;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Common.Services;

namespace Kratos.Api.Startup;

public static class Services
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

    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
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
        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(DataSource.OfPostgres(connectionString)));
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
