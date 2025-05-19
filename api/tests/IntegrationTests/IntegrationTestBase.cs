using Microsoft.Extensions.DependencyInjection;

using Xunit.Abstractions;

using Kratos.Api.Tests.Utils;

namespace Kratos.Api.Tests.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected TestWebAppFactory Factory { get; }
    protected HttpClient Http { get; }
    protected IServiceScope Scope { get; }
    protected ITestOutputHelper? Output { get; }

    private bool testsPassed = true;

    protected IntegrationTestBase(string connectionString, ITestOutputHelper? output = null)
    {
        PostgresUtils.RecreateDatabaseAsync(connectionString).Wait();

        Factory = new TestWebAppFactory(connectionString);
        Http = Factory.CreateClient();
        Scope = Factory.Services.CreateAsyncScope();
        Output = output;
    }

    public virtual async Task InitializeAsync()
    {
        await Factory.EnsureDatabaseCreatedAsync();
    }

    public virtual async Task DisposeAsync()
    {
        if (testsPassed)
        {
            await Factory.CleanUpDatabaseAsync();
        }
        else
        {
            string message = $"Tests failed. Database preserved at: {Factory.ConnectionString}";
            Output?.WriteLine(message);
        }

        Scope.Dispose();
        Http.Dispose();
        Factory.Dispose();
    }

    protected void MarkTestsFailed()
    {
        testsPassed = false;
    }

    protected void PreserveDatabase()
    {
        testsPassed = false;
    }
}
