using System.Net.Http.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Kratos.Api.Common.Types;
using Kratos.Api.Database;
using Kratos.Api.Features.Auth.Google;
using static Kratos.Api.Tests.IntegrationTests.Constants;

namespace Kratos.Api.Tests.IntegrationTests.Auth.Google;

public class MultipleSessions() : IntegrationTestBase(ConnectionStrings.TestDbAuthMultipleSessions)
{
    [Fact]
    public async Task WhenUserLogsInFromDifferentClients_MultipleSessionsShouldBeCreated()
    {
        try
        {
            string sessionId1 = await AuthGoogle_UserLogsIn_WithCorrectToken();
            string sessionId2 = await AuthGoogle_UserLogsIn_WithCorrectToken();

            Assert.NotEqual(sessionId1, sessionId2);

            DatabaseContext database = Scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            int sessionCount = await database.UserSessions
                .Where(us => us.SessionId == sessionId1 || us.SessionId == sessionId2)
                .CountAsync();

            Assert.Equal(2, sessionCount);
        }
        catch
        {
            MarkTestsFailed();
            throw;
        }
    }

    private async Task<string> AuthGoogle_UserLogsIn_WithCorrectToken(string? sessionId = null)
    {
        // Arrange
        Request request = new()
        {
            GoogleToken = GoogleTokens.CorrectToken,
            SessionId = sessionId
        };

        // Act
        HttpResponseMessage response = await Http.PostAsJsonAsync(Urls.GoogleLogin, request);
        GeneratedTokens? payload = await response.Content.ReadFromJsonAsync<GeneratedTokens>();

        // Assert
        Assert.NotNull(payload);
        Assert.NotNull(payload.SessionId);

        return payload.SessionId;
    }
}
