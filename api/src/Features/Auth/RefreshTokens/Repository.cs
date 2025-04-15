using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Auth.RefreshTokens;

public interface IRepository
{
    Task<UserSession?> GetSessionForUserAsync(long userId, string sessionId, CancellationToken cancellationToken);
    Task UpdateRefreshToken(UserSession userSession, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<UserSession?> GetSessionForUserAsync(long userId, string sessionId, CancellationToken cancellationToken)
    {
        UserSession? userSession = await database.UserSessions.FirstOrDefaultAsync(x => x.UserId == userId && x.SessionId == sessionId, cancellationToken);
        return userSession;
    }

    public async Task UpdateRefreshToken(UserSession userSession, CancellationToken cancellationToken)
    {
        database.UserSessions.Update(userSession);
        await database.SaveChangesAsync(cancellationToken);
    }
}
