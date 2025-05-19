using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Common.Utils;
using Kratos.Api.Database;

namespace Kratos.Api.Features.Articles.GetById;

public interface IRepository
{
    Task<Article?> GetByIdAsync(long articleId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Article?> GetByIdAsync(long articleId, CancellationToken cancellationToken)
    {
        return await database.Articles
            .AsNoTracking()
            .Where(x => x.Id == articleId && !x.IsDeleted)
            .Select(x => new Article(
                x.Id,
                x.Title,
                x.Summary,
                x.Content,
                x.ImageFileName, 
                x.ApprovalStatus.ToString().ToTitleCase(),
                x.CreatedByUser.Profile!.FullName,
                x.CreatedByUser.Profile!.DisplayPictureFileName!,
                x.CreatedAt.ToString("yyyy-MM-dd")
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
