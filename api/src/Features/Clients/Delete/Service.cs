using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Clients.Delete;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> DeleteAsync(long clientId, CancellationToken cancellationToken)
    {
        bool deleted = await repo.DeleteAsync(clientId, cancellationToken);
        if (!deleted)
        {
            return Result.NotFoundError("Client not found");
        }

        return Result.Success();
    }
}
