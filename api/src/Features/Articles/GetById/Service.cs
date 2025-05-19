using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Articles.GetById;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<Article>> GetByIdAsync(long articleId, CancellationToken cancellationToken)
    {
        Article? article = await repo.GetByIdAsync(articleId, cancellationToken);
        if (article is null)
        {
            return Result.NotFoundError("Specified article not found");
        }

        return Result.Success(article);
    }
}
