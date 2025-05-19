using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.Delete;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> DeleteAsync(long serviceId, CancellationToken cancellationToken)
    {
        bool exists = await repo.ExistsAsync(serviceId, cancellationToken);
        if (!exists)
        {
            return Result.NotFoundError("Service not found");
        }

        bool hasChildren = await repo.HasChildrenAsync(serviceId, cancellationToken);
        if (hasChildren)
        {
            return Result.CannotProcessError("Service has children");
        }

        await repo.DeleteAsync(serviceId, cancellationToken);
        return Result.Success();
    }
}
