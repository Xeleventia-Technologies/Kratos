using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Feeback.Update;

public interface IRepository
{
    Task<Feedback?> GetForUser(long userId, CancellationToken cancellationToken);
    Task UpdateAsync(Feedback feedback, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Feedback?> GetForUser(long userId, CancellationToken cancellationToken)
    {
        return await database.Feedbacks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Feedback feedback, CancellationToken cancellationToken)
    {
        database.Feedbacks.Update(feedback);
        await database.SaveChangesAsync(cancellationToken);
    }
}
