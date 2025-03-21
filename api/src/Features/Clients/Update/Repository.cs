using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Clients.Update;

public interface IRepository
{
    Task<Client?> GetByIdAsync(long clientId, CancellationToken cancellationToken);
    Task UpdateAsync(Client existingClient, Client newClient, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Client?> GetByIdAsync(long clientId, CancellationToken cancellationToken)
    {
        Client? existingClient = await database.Clients.FirstOrDefaultAsync(c => c.Id == clientId, cancellationToken);
        return existingClient;
    }

    public async Task UpdateAsync(Client existingClient, Client newClient, CancellationToken cancellationToken)
    {
        existingClient.CloudStorageLink = newClient.CloudStorageLink;

        await database.SaveChangesAsync(cancellationToken);
    }
}
