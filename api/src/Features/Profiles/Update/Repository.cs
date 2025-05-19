using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Profiles.Update;

public interface IRepository
{
    Task<Profile?> GetForUser(long userId, CancellationToken cancellationToken);
    Task UpdateAsync(Profile profile, string? displayPictureFileName, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Profile?> GetForUser(long userId, CancellationToken cancellationToken)
    {
        return await database.Profiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task UpdateAsync(Profile profile, string? displayPictureFileName, CancellationToken cancellationToken)
    {
        profile.DisplayPictureFileName = displayPictureFileName;
        database.Profiles.Update(profile);

        await database.SaveChangesAsync(cancellationToken);
    }
}
