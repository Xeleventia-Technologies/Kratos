using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.Delete;

public interface IRepository
{
    Task<int> DeleteAsync(long serviceId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
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
