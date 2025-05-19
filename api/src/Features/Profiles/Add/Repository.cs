using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Profiles.Add;

public interface IRepository
{
    Task<bool> ExistsForUser(long userId, CancellationToken cancellationToken);
    Task AddAsync(Profile profile, long userId, string? displayPictureFileName, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> ExistsForUser(long userId, CancellationToken cancellationToken)
    {
        return await database.Profiles.AnyAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(Profile profile, long userId, string? displayPictureFileName, CancellationToken cancellationToken)
    {
        profile.UserId = userId;
        profile.DisplayPictureFileName = displayPictureFileName;

        await database.Profiles.AddAsync(profile, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);
    }
}
