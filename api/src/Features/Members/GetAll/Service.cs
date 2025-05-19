using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Members.GetAll;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<List<Projections.Member>>> GetAllAsync(CancellationToken cancellationToken) => Result.Success(await repo.GetAllAsync(cancellationToken));
}
