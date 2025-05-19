using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Articles.GetApproved;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Response>> GetApprovedAsync(FilterGetParams filters, CancellationToken cancellationToken) 
    {
        Response response = await repo.GetApprovedAsync(filters.AsFilterParams(), cancellationToken);
        return Result.Success(response);
    }
}
