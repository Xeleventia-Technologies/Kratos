using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Articles.GetForUser;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Response>> GetForUserAsync(long userId, FilterGetParams filters, CancellationToken cancellationToken) 
    {
        Response response = await repo.GetForUserAsync(userId, filters.AsFilterParams(), cancellationToken);
        return Result.Success(response);
    }
}
