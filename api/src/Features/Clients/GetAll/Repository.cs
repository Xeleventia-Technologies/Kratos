using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace Kratos.Api.Features.Clients.GetAll;

public interface IRepository
{
    Task<List<Projections.Client>> GetAllAsync(CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<List<Projections.Client>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Projections.Client> clients = await database.Clients
            .Select(c => new Projections.Client()
            {
                Id = c.Id,
                CloudStorageLink = c.CloudStorageLink,
                Username = c.User.UserName!
            })
            .ToListAsync();

        return clients;
    }
}
