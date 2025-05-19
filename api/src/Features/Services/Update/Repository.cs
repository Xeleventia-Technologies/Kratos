using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.Update;

public interface IRepository
{
    Task<bool> SeoFriendlyNameExistsAsync(long id, string seoFriendlyServiceName, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    Task<Database.Models.Service?> GetByIdAsync(long serviceId, CancellationToken cancellationToken);
    Task UpdateAsync(Database.Models.Service service, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> SeoFriendlyNameExistsAsync(long id, string seoFriendlyServiceName, CancellationToken cancellationToken)
    {
        return await database.Services.AnyAsync(s => EF.Functions.ILike(s.SeoFriendlyName, seoFriendlyServiceName) && s.Id != id  && !s.IsDeleted, cancellationToken);
    }
    
    public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
    {
        return await database.Services.AnyAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);
    }

    public async Task<Database.Models.Service?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await database.Services.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);
    }

    public async Task UpdateAsync(Database.Models.Service service, CancellationToken cancellationToken)
    {
        database.Services.Update(service);
        await database.SaveChangesAsync(cancellationToken);
    }
}
