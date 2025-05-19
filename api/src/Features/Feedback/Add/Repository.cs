using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Feeback.Add;

public interface IRepository
{
    Task<bool> ExistsForUser(long userId, CancellationToken cancellationToken);
    Task AddAsync(long userId, Feedback feedback, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> ExistsForUser(long userId, CancellationToken cancellationToken)
    {
        return await database.Feedbacks.AnyAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(long userId, Feedback feedback, CancellationToken cancellationToken)
    {
        feedback.UserId = userId;
        database.Feedbacks.Add(feedback);

        await database.SaveChangesAsync(cancellationToken);
    }
}
