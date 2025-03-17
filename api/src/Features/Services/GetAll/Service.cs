using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.GetAll;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<List<Projections.Service>>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Projections.Service> services = await repo.GetAllAsync(cancellationToken);
        return Result.Success(services);
    }
}
