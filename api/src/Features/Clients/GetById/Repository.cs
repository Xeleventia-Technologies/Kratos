using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Clients.GetById;

public interface IRepository
{
    Task<Projections.Client?> GetByIdAsync(long clientId, CancellationToken cancellationToken);
}
public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Projections.Client?> GetByIdAsync(long clientId, CancellationToken cancellationToken)
    {
        Projections.Client? client = await database.Clients
            .Where(c => c.Id == clientId)
            .Select(c => new Projections.Client()
            {
                Id = c.Id,
                CloudStorageLink = c.CloudStorageLink,
                Username = c.User.UserName!
            })
            .FirstOrDefaultAsync(cancellationToken);

        return client;
    }
}
