using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Types;
using Kratos.Api.Database;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Features.Auth.Google;
using static Kratos.Api.Tests.IntegrationTests.Constants;

namespace Kratos.Api.Tests.IntegrationTests.Auth.Google;

public class GoogleWeb() : IntegrationTestBase(ConnectionStrings.TestDbAuthGoogleWeb)
{
    [Fact]
    public async Task AuthGoogleWeb_UserLogsIn()
    {
        try
        {
            // Should not be able to log in with incorrect token
            HttpStatusCode incorrectTokenStatus = await AuthGoogle_UserLogsIn_WithIncorrectToken();
            Assert.Equal(HttpStatusCode.Unauthorized, incorrectTokenStatus);

            // Non-existing user should be able to log in with correct token and return "201 Created"
            HttpStatusCode createdStatus = await AuthGoogle_UserLogsIn_WithCorrectToken();
            Assert.Equal(HttpStatusCode.Created, createdStatus);

            // Existing user should be able to log in with correct token and return "200 OK"
            HttpStatusCode okStatus = await AuthGoogle_UserLogsIn_WithCorrectToken();
            Assert.Equal(HttpStatusCode.OK, okStatus);
        }
        catch
        {
            MarkTestsFailed();
            throw;
        }
    }

    private async Task<HttpStatusCode> AuthGoogle_UserLogsIn_WithIncorrectToken()
    {
        // Arrange
        Request request = new()
        {
            GoogleToken = GoogleTokens.IncorrectToken,
            SessionId = null
        };

        // Act
        HttpResponseMessage response = await Http.PostAsJsonAsync(Urls.GoogleLoginWeb, request);
        return response.StatusCode;
    }

    private async Task<HttpStatusCode> AuthGoogle_UserLogsIn_WithCorrectToken()
    {
        // Arrange
        Request request = new()
        {
            GoogleToken = GoogleTokens.CorrectToken,
            SessionId = null
        };

        // Act
        HttpResponseMessage response = await Http.PostAsJsonAsync(Urls.GoogleLoginWeb, request);

        // Assert
        OnlyAccessToken? payload = await response.Content.ReadFromJsonAsync<OnlyAccessToken>();

        Assert.NotNull(payload);
        Assert.NotNull(payload.AccessToken);

        string? refreshToken = response.GetCookieValue(TokenType.RefreshToken.Name);
        string? sessionId = response.GetCookieValue(TokenType.SessionId.Name);

        Assert.NotNull(refreshToken);
        Assert.NotNull(sessionId);

        UserManager<User> userManager = Scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        User? user = await userManager.FindByEmailAsync(MockGoogleUser.Email);
        Assert.NotNull(user);

        bool hasUserRole = await userManager.IsInRoleAsync(user, Common.Constants.Role.User.Name);
        Assert.True(hasUserRole);

        DatabaseContext database = Scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        UserSession? userSession = await database.UserSessions
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == user.Id && u.RefreshToken == refreshToken && u.SessionId == sessionId);

        Assert.NotNull(userSession);

        return response.StatusCode;
    }
}
