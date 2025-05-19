using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

using Kratos.Api.Common.Services;
using Kratos.Api.Database;

namespace Kratos.Api.Tests.IntegrationTests;

public class TestWebAppFactory(string connectionString) : WebApplicationFactory<Program>
{
    public string ConnectionString { get => connectionString; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.Sources.Clear();
            config.AddJsonFile("appsettings.Testing.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<DatabaseContext>>();
            services.RemoveAll<IGoogleTokenService>();

            services.AddDbContext<DatabaseContext>((serviceProvider, options) =>
            {
                options.UseNpgsql(DataSource.OfPostgres(connectionString));
            });

            services.AddScoped<IGoogleTokenService>(_ =>
            {
                Mocks.Services.GoogleTokenService mockGoogleTokenService = new();
                mockGoogleTokenService.SetMockGoogleUser(Constants.MockGoogleUser);

                return mockGoogleTokenService;
            });
        });
    }

    public async Task EnsureDatabaseCreatedAsync()
    {
        using IServiceScope scope = Services.CreateScope();
        DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public async Task CleanUpDatabaseAsync()
    {
        using IServiceScope scope = Services.CreateScope();
        DatabaseContext dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await dbContext.Database.EnsureDeletedAsync();
    }
}
