using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models.Identity;

using static Kratos.Api.Common.Constants.Auth;

namespace Kratos.Api.Features.Auth.Login;

public interface IRepository
{
    Task<UserToken?> GetUserTokenAsync(long userId, string sessionId, CancellationToken cancellationToken);
    Task AddOrUpdateUserTokenAsync(UserToken userToken, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<UserToken?> GetUserTokenAsync(long userId, string sessionId, CancellationToken cancellationToken)
    {
        return await database.UserTokens.FirstOrDefaultAsync(x =>
            x.UserId == userId &&
            x.SessionId == sessionId &&
            x.LoginProvider == LoginProvider.Self.Name
        , cancellationToken);
    }

    public async Task AddOrUpdateUserTokenAsync(UserToken userToken, CancellationToken cancellationToken)
    {
        database.UserTokens.Update(userToken);
        await database.SaveChangesAsync(cancellationToken);
    }
}
