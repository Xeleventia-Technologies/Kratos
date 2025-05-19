using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.GetAll;

public interface IRepository
{
    Task<List<Projections.Service>> GetAllAsync(CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<List<Projections.Service>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await database.Services
            .Where(s => !s.IsDeleted)
            .OrderBy(x => x.Id)
            .Select(s => new Projections.Service(
                s.Id,
                s.Name,
                s.Summary,
                s.Description,
                s.ImageFileName,
                s.SeoFriendlyName,
                s.ParentService == null ? null : s.ParentService.Id,
                s.ParentService == null ? null : s.ParentService.Name,
                s.CreatedAt
            ))
            .ToListAsync(cancellationToken);
    }
}
