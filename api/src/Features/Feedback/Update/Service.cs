using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Feeback.Update;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> UpdateAsync(long userId, Request request, CancellationToken cancellationToken)
    {
        Feedback? existingFeedback = await repo.GetForUser(userId, cancellationToken);
        if (existingFeedback is null)
        {
            return Result.NotFoundError("Specified user has not given any feedback");
        }

        existingFeedback.UpdateFrom(request);
        await repo.UpdateAsync(existingFeedback, cancellationToken);

        return Result.Success();
    }
}
