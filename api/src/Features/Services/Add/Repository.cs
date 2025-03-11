using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Services.Add;

public interface IRepository
{
    Task AddServiceAsync(Database.Models.Service service, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task AddServiceAsync(Database.Models.Service service, CancellationToken cancellationToken)
    {
        database.Services.Add(service);
        await database.SaveChangesAsync(cancellationToken);
    }
}
