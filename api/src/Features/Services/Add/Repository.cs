using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.Add;

public interface IRepository
{
    Task<bool> ExistsAsync(string serviceName, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(long serviceId, CancellationToken cancellationToken);
    Task AddServiceAsync(Database.Models.Service service, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> ExistsAsync(string seoFriendlyServiceName, CancellationToken cancellationToken)
    {
        return await database.Services.AnyAsync(s => EF.Functions.ILike(s.SeoFriendlyName, seoFriendlyServiceName) && !s.IsDeleted, cancellationToken);
    }

    public async Task<bool> ExistsAsync(long serviceId, CancellationToken cancellationToken)
    {
        return await database.Services.AnyAsync(s => s.Id == serviceId && !s.IsDeleted, cancellationToken);
    }

    public async Task AddServiceAsync(Database.Models.Service service, CancellationToken cancellationToken)
    {
        await database.Services.AddAsync(service, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);
    }
}
