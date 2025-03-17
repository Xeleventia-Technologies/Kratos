using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.GetById;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Projections.Service>> GetByIdAsync(long serviceId, CancellationToken cancellationToken)
    {
        Projections.Service? service = await repo.GetByIdAsync(serviceId, cancellationToken);
        if (service is null)
        {
            return Result.NotFoundError("Service not found");
        }

        return Result.Success(service);
    }
}
