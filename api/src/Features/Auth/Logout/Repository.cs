using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Auth.Logout;

public interface IRepository
{
    Task<int> DeleteSessionAsync(string sessionId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<int> DeleteSessionAsync(string sessionId, CancellationToken cancellationToken)
    {
        int rowsAffected = await database.UserSessions
            .Where(x => x.SessionId == sessionId)
            .ExecuteDeleteAsync(cancellationToken);

        return rowsAffected;
    }
}
