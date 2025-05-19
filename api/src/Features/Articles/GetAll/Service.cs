using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Articles.GetAll;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Response>> GetAllAsync(FilterGetParams filters, CancellationToken cancellationToken) 
    {
        Response response = await repo.GetAllAsync(filters.AsFilterParams(), cancellationToken);
        return Result.Success(response);
    }
}
