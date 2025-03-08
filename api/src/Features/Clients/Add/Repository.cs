using Kratos.Api.Database;
using Kratos.Api.Database.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kratos.Api.Features.Clients.Add;

public interface IRepository
{
    Task<bool> ExistsForUser(long UserId, CancellationToken cancellationToken);
    Task AddAsync(Client client, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> ExistsForUser(long UserId, CancellationToken cancellationToken)
    {
        return await database.Clients.AnyAsync(x => x.UserId == UserId, cancellationToken: cancellationToken);
    }

    public Task AddAsync(Client client, CancellationToken cancellationToken)
    {
        database.Clients.Add(client);
        return database.SaveChangesAsync(cancellationToken);
    }

}
