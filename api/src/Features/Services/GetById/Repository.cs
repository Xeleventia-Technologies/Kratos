using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.GetById;

public interface IRepository
{
    Task<Projections.ServiceForUi?> GetBySeoFriendlyNameAsync(string serviceName, CancellationToken cancellationToken);
    Task<Projections.Service?> GetByIdAsync(long serviceId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Projections.ServiceForUi?> GetBySeoFriendlyNameAsync(string serviceName, CancellationToken cancellationToken)
    {
        Projections.ServiceForUi? services = await database.Services
            .Where(s => EF.Functions.ILike(s.SeoFriendlyName, serviceName) && !s.IsDeleted)
            .Select(s => new Projections.ServiceForUi(
                s.Id,
                s.Name,
                s.Summary,
                s.Description,
                s.ImageFileName,
                s.SeoFriendlyName,
                s.ChildServices.Select(cs => new Projections.ServiceForUi(
                    cs.Id,
                    cs.Name,
                    cs.Summary,
                    cs.Description,
                    cs.ImageFileName,
                    cs.SeoFriendlyName,
                    null
                )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        return services;
    }

    public async Task<Projections.Service?> GetByIdAsync(long serviceId, CancellationToken cancellationToken)
    {
        Projections.Service? services = await database.Services
            .Where(s => s.Id == serviceId && !s.IsDeleted)
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
            .FirstOrDefaultAsync(cancellationToken);

        return services;
    }
}
