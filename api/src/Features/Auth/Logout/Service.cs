using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Auth.Logout;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> LogoutAsync(string sessionId, CancellationToken cancellationToken)
    {
        int rowsAffected = await repo.DeleteSessionAsync(sessionId, cancellationToken);
        return rowsAffected > 0
            ? Result.Success()
            : Result.NotFoundError("Session not found");
    }
}
