using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Members.GetAll;

public interface IRepository
{
    Task<List<Projections.Member>> GetAllAsync(CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<List<Projections.Member>> GetAllAsync(CancellationToken cancellationToken) => await database.Members
        .OrderBy(x => x.Id)
        .Select(x => new Projections.Member(x.Id, x.FullName, x.Bio, x.Position, x.DisplayPictureFileName))
        .ToListAsync(cancellationToken);
}
