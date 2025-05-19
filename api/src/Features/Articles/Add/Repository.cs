using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.Add;

public interface IRepository
{
    Task AddAsync(long userId, Article article, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task AddAsync(long userId, Article article, CancellationToken cancellationToken)
    {
        article.CreatedByUserId = userId;
        
        await database.Articles.AddAsync(article, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);
    }
}
