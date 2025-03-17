using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.Update;

public interface IRepository
{
    Task<Database.Models.Service?> GetByIdAsync(long serviceId, CancellationToken cancellationToken);
    Task UpdateAsync(Database.Models.Service existingService, Database.Models.Service newService, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Database.Models.Service?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        Database.Models.Service? existingService = await database.Services.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);
        return existingService;
    }

    public async Task UpdateAsync(Database.Models.Service existingService, Database.Models.Service newService, CancellationToken cancellationToken)
    {
        existingService.Name = newService.Name;
        existingService.Summary = newService.Summary;
        existingService.Description = newService.Description;
        existingService.UpdatedAt = DateTime.UtcNow;

        if (newService.ImageFileName is not null)
        {
            existingService.ImageFileName = newService.ImageFileName;
        }
        
        await database.SaveChangesAsync(cancellationToken);
    }
}
