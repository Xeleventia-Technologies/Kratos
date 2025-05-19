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
            .Where(c => c.Id == clientId && !c.IsDeleted)
            .Select(c => new Projections.Client(
                c.Id,
                c.UserId,
                c.User.Profile!.FullName,
                c.User.Email!,
                c.User.Profile!.MobileNumber,
                c.CloudStorageLink
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return client;
    }
}
