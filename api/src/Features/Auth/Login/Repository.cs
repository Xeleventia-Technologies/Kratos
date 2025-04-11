using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Auth.Login;

public interface IRepository
{
    Task<UserSession?> GetUserSessionAsync(long userId, string sessionId, CancellationToken cancellationToken);
    Task AddOrUpdateUserSessionAsync(UserSession userSession, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<UserSession?> GetUserSessionAsync(long userId, string sessionId, CancellationToken cancellationToken)
    {
        return await database.UserSessions.FirstOrDefaultAsync(x =>
            x.UserId == userId &&
            x.SessionId == sessionId &&
            x.LoggedInWith == Enums.LoggedInWith.Email
        , cancellationToken);
    }

    public async Task AddOrUpdateUserSessionAsync(UserSession userSession, CancellationToken cancellationToken)
    {
        database.UserSessions.Update(userSession);
        await database.SaveChangesAsync(cancellationToken);
    }
}
