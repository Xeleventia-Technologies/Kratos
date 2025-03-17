using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.GetById;

public interface IRepository
{
    Task<Projections.Service?> GetByIdAsync(long serviceId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Projections.Service?> GetByIdAsync(long serviceId, CancellationToken cancellationToken)
    {
        Projections.Service? services = await database.Services
            .Where(s => s.Id == serviceId && !s.IsDeleted)
            .Select(s => new Projections.Service()
            {
                Id = s.Id,
                Name = s.Name,
                Summary = s.Summary,
                Description = s.Description,
                ImageFileName = s.ImageFileName
            })
            .FirstOrDefaultAsync(cancellationToken);
            
        return services;
    }
}
