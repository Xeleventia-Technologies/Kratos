using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Articles.GetById;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Article>> GetByIdAsync(string? userRole, long articleId, CancellationToken cancellationToken)
    {
        Article? article = await repo.GetByIdAsync(articleId, cancellationToken);
        if (article is null)
        {
            return Result.NotFoundError("Specified article not found");
        }

        if (userRole is not null && userRole == Role.Admin.Name)
        {
            return Result.Success(article);
        }

        if (article.ApprovalStatus != Enums.ArticleApprovalStatus.Approved.ToString())
        {
            return Result.NotFoundError("Specified article not found");
        }

        return Result.Success(article);
    }
}
