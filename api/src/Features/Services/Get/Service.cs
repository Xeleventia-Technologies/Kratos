using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.Get;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<List<Projections.Service>>> GetAsync(GetParams getParams, CancellationToken cancellationToken)
    {
        List<Projections.Service> services = await repo.GetAsync(getParams, cancellationToken);
        return Result.Success(services);
    }
}
