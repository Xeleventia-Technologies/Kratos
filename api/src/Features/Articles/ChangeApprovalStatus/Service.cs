using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.ChangeApprovalStatus;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> ChangeApprovalStatusAsync(long articleId, string status, CancellationToken cancellationToken)
    {
        int rowsAffected = await repo.ChangeApprovalStatusAsync(
            articleId, 
            Enum.Parse<Enums.ArticleApprovalStatus>(status, ignoreCase: true),
            cancellationToken
        );

        return rowsAffected > 0
            ? Result.Success()
            : Result.NotFoundError("Sepcified article not found");
    }
}
