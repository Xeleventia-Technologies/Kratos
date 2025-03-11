using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kratos.Api.Features.Clients.Add;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> AddAsync(Client client, CancellationToken cancellationToken)
    {
        if (await repo.ExistsForUser(client.UserId, cancellationToken))
        {
            return Result.ConflictError("Client already exists for user");
        }

        await repo.AddAsync(client, cancellationToken);
        return Result.Success();
    }
}