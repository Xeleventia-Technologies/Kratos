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
        List<Projections.Service> services = await database.Services
            .Where(s => !s.IsDeleted)
            .Select(s => new Projections.Service()
            {
                Id = s.Id,
                Name = s.Name,
                Summary = s.Summary,
                Description = s.Description,
                ImageFileName = s.ImageFileName
            })
            .ToListAsync(cancellationToken);
            
        return services;
    }
}
