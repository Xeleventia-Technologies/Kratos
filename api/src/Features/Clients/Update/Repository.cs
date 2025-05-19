using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Clients.Update;

public interface IRepository
{
    Task<Profile?> GetProfileForUserAsync(long userId, CancellationToken cancellationToken);
    Task<Client?> GetByIdAsync(long clientId, CancellationToken cancellationToken);
    Task UpdateAsync(Client client, Profile profile, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Profile?> GetProfileForUserAsync(long userId, CancellationToken cancellationToken)
    {
        return await database.Profiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task<Client?> GetByIdAsync(long clientId, CancellationToken cancellationToken)
    {
        Client? existingClient = await database.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == clientId && !c.IsDeleted, cancellationToken);

        return existingClient;
    }

    public async Task UpdateAsync(Client client, Profile profile, CancellationToken cancellationToken)
    {
        database.Profiles.Update(profile);
        database.Clients.Update(client);
        await database.SaveChangesAsync(cancellationToken);
    }
}
