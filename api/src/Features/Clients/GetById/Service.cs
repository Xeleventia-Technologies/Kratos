using Kratos.Api.Common.Types;
using Microsoft.AspNetCore.Mvc;

namespace Kratos.Api.Features.Clients.GetById;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Projections.Client>> GetByIdAsync(long clientId, CancellationToken cancellationToken)
    {
        Projections.Client? client = await repo.GetByIdAsync(clientId, cancellationToken);
        if (client is null)
        {
            return Result.NotFoundError("Client not found");
        }

        return Result.Success(client);
    }
}