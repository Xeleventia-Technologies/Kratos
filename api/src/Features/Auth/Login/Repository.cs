using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Auth.Login;

public interface IRepository
{
    Task<Profile?> GetUserProfileAsync(long userId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Profile?> GetUserProfileAsync(long userId, CancellationToken cancellationToken)
    {
        return await database.Profiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }
}
