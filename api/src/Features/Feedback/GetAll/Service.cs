using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Feeback.GetAll;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<List<Projections.Feedback>>> GetAllAsync(CancellationToken cancellationToken) => Result.Success(await repo.GetAllAsync(cancellationToken));
}
