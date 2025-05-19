using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.Update;

public interface IRepository
{
    Task<Article?> GetByIdAsync(long articleId, CancellationToken cancellationToken);
    Task UpdateAsync(Article article, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public Task<Article?> GetByIdAsync(long articleId, CancellationToken cancellationToken)
    {
        return database.Articles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == articleId, cancellationToken);
    }

    public Task UpdateAsync(Article article, CancellationToken cancellationToken)
    {
        database.Articles.Update(article);
        return database.SaveChangesAsync(cancellationToken);
    }
}
