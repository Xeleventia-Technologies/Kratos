using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Clients.GetAll;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<List<Projections.Client>>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<Projections.Client> clients = await repo.GetAllAsync(cancellationToken);
        return Result.Success(clients);
    }
}
