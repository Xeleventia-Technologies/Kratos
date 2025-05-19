using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Profiles.Get;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Projections.Profile>> GetForUser(long userId, CancellationToken cancellationToken)
    {
        Projections.Profile? profile = await repo.GetForUser(userId, cancellationToken);
        if (profile is null)
        {
            return Result.NotFoundError("Profile not found for user");
        }

        return Result.Success(profile);
    }
}
