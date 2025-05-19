using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Clients.Delete;

public interface IRepository
{
    Task<bool> DeleteAsync(long clientId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> DeleteAsync(long clientId, CancellationToken cancellationToken)
    {
        int rowsAffected = await database.Clients
            .Where(c => c.Id == clientId && !c.IsDeleted)
            .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsDeleted, true), cancellationToken);

        return rowsAffected > 0;
    }
}
