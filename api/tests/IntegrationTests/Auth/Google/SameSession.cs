using System.Net.Http.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Kratos.Api.Database;
using Kratos.Api.Common.Types;
using Kratos.Api.Features.Auth.Google;
using static Kratos.Api.Tests.IntegrationTests.Constants;

namespace Kratos.Api.Tests.IntegrationTests.Auth.Google;

public class SameSession() : IntegrationTestBase(ConnectionStrings.TestDbAuthSameSessions)
{
    [Fact]
    public async Task WhenUserLogsInFromOneClientMultipleTimes_MultipleSessionsShouldNotBeCreated()
    {
        try
        {
            string sessionId1 = await AuthGoogle_UserLogsIn_WithCorrectToken();
            string sessionId2 = await AuthGoogle_UserLogsIn_WithCorrectToken(sessionId1);

            Assert.Equal(sessionId1, sessionId2);

            DatabaseContext database = Scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            int sessionCount = await database.UserSessions
                .Where(us => us.SessionId == sessionId1)
                .CountAsync();
                
            Assert.Equal(1, sessionCount);
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