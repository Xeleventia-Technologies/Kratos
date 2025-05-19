using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Feeback.Add;

public class Service(
    [FromServices] IRepository repo
)
{
    public async Task<Result> AddAsync(long userId, Feedback feedback, CancellationToken cancellationToken)
    {
        bool feedbackExists = await repo.ExistsForUser(userId, cancellationToken);
        if (feedbackExists)
        {
            return Result.ConflictError("Feedback already exists for user");
        }

        await repo.AddAsync(userId, feedback, cancellationToken);
        return Result.Success();
    }
}
