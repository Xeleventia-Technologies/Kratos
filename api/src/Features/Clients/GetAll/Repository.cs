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
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.Id)
            .Select(c => new Projections.Client(
                c.Id,
                c.UserId,
                c.User.Profile!.FullName,
                c.User.Email!,
                c.User.Profile!.MobileNumber,
                c.CloudStorageLink
            ))
            .ToListAsync(cancellationToken);

        return clients;
    }
}
