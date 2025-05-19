using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Feeback.GetAll;

public interface IRepository
{
    Task<List<Projections.Feedback>> GetAllAsync(CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<List<Projections.Feedback>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await database.Feedbacks
            .OrderBy(x => x.Id)
            .Select(x => new Projections.Feedback(x.Id, x.User.Profile != null ? x.User.Profile.FullName : "[No Name]", x.OutOfFiveRating, x.Comment))
            .ToListAsync(cancellationToken);
    }
}
