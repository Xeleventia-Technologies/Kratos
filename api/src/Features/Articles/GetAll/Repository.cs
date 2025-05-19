using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;
using Kratos.Api.Common.Utils;

namespace Kratos.Api.Features.Articles.GetAll;

public interface IRepository
{
    Task<Response> GetAllAsync(FilterParams filters, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Response> GetAllAsync(FilterParams filters, CancellationToken cancellationToken)
    {
        IQueryable<Article> articles = database.Articles
            .AsNoTracking()
            .Where(x => !x.IsDeleted);

        if (filters.Search is not null)
        {
            articles = articles.Where(x =>
                EF.Functions.ILike(x.Title, $"%{filters.Search}%") ||
                EF.Functions.ILike(x.Summary, $"%{filters.Search}%") ||
                EF.Functions.ILike(x.Content, $"%{filters.Search}%") ||
                EF.Functions.ILike(x.CreatedByUser.Profile!.FullName, $"%{filters.Search}%")
            );
        }

        if (filters.Approval is not null)
        {
            articles = articles.Where(x => x.ApprovalStatus == filters.Approval);
        }

        if (filters.From is not null)
        {
            DateTimeOffset from = filters.From.Value.Date.WithOffset();
            articles = articles.Where(x => x.CreatedAt >= from);
        }

        if (filters.To is not null)
        {
            DateTimeOffset to = filters.To.Value.Date.AddDays(1).AddTicks(-1).WithOffset();
            articles = articles.Where(x => x.CreatedAt <= to);
        }

        int totalCount = await articles.CountAsync(cancellationToken); 

        articles = articles.OrderByDescending(x => x.CreatedAt)
            .Skip(filters.Count * (filters.Page - 1))
            .Take(filters.Count);

        return new Response
        {
            TotalCount = totalCount,
            Articles = await articles
                .Select(x => new Response.Article(
                    x.Id,
                    x.Title,
                    x.Summary,
                    x.ApprovalStatus.ToString().ToTitleCase(),
                    x.CreatedByUser.Profile!.FullName,
                    x.CreatedAt.ToString("yyyy-MM-dd")
                ))
                .ToListAsync(cancellationToken)
        };
    }
}
