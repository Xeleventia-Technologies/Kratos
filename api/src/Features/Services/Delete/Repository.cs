using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.Delete;

public interface IRepository
{
    Task<bool> ExistsAsync(long serviceId, CancellationToken cancellationToken);
    Task<bool> HasChildrenAsync(long serviceId, CancellationToken cancellationToken);
    Task<int> DeleteAsync(long serviceId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> ExistsAsync(long serviceId, CancellationToken cancellationToken)
    {
        return await database.Services.AnyAsync(s => s.Id == serviceId && !s.IsDeleted, cancellationToken);
    }

    public async Task<bool> HasChildrenAsync(long serviceId, CancellationToken cancellationToken)
    {
        return await database.Services.AnyAsync(s => s.ParentServiceId == serviceId && !s.IsDeleted, cancellationToken);
    }

    public async Task<int> DeleteAsync(long serviceId, CancellationToken cancellationToken)
    {
        int rowsAffected = await database.Services
            .Where(s => s.Id == serviceId && !s.IsDeleted)
            .ExecuteUpdateAsync(
                s => s.SetProperty(s => s.IsDeleted, true),
                cancellationToken
            );

        return rowsAffected;
    }
}
