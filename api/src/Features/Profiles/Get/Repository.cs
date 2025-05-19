using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Profiles.Get;

public interface IRepository
{
    Task<Projections.Profile?> GetForUser(long userId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Projections.Profile?> GetForUser(long userId, CancellationToken cancellationToken)
    {
        Projections.Profile? profile = await database.Profiles
            .Where(x => x.UserId == userId)
            .Select(x => new Projections.Profile(x.FullName, x.MobileNumber, x.Bio, x.DisplayPictureFileName))
            .FirstOrDefaultAsync(cancellationToken);

        return profile;
    }
}
