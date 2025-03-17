using Kratos.Api.Common.Types;
using Microsoft.AspNetCore.Mvc;

namespace Kratos.Api.Features.Services.Delete;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> DeleteAsync(long serviceId, CancellationToken cancellationToken)
    {
        int rowsAffected = await repo.DeleteAsync(serviceId, cancellationToken);
        if (rowsAffected == 0)
        {
            return Result.NotFoundError("Service not found");
        }

        return Result.Success();
    }
}
