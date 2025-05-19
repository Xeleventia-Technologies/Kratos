using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.Get;

public interface IRepository
{
    Task<List<Projections.Service>> GetAsync(CancellationToken cancellationToken);

    Task<List<Projections.Service>> GetAsync(GetParams getParams, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<List<Projections.Service>> GetAsync(CancellationToken cancellationToken)
    {
        return await GetAsync(new GetParams(), cancellationToken);
    }

    public async Task<List<Projections.Service>> GetAsync(GetParams getParams, CancellationToken cancellationToken)
    {
        List<Projections.Service> services = await database.Services
            .Where(s => s.ParentServiceId == getParams.ParentId && !s.IsDeleted)
            .OrderBy(x => x.Id)
            .Select(s => new Projections.Service(
                s.Id,
                s.Name,
                s.Summary,
                s.Description,
                s.ImageFileName,
                s.SeoFriendlyName,
                s.ParentService == null ? null : s.ParentService.Name,
                s.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return services;
    }
}
