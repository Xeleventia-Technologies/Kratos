using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kratos.Api.Features.Clients.Update;

public class Service ([FromServices] IRepository repo)
{
    public async Task<Result> UpdateAsync(long clientId, Client newClient, CancellationToken cancellationToken)
    {   
        Client? existingClient = await repo.GetByIdAsync(clientId, cancellationToken);
        if (existingClient == null)
        {
            return Result.NotFoundError("Client not found");
        }

        await repo.UpdateAsync(existingClient, newClient, cancellationToken);
        return Result.Success();
    }
}