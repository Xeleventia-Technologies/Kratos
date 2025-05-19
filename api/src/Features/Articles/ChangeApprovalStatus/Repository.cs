using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.ChangeApprovalStatus;

public interface IRepository
{
    Task<int> ChangeApprovalStatusAsync(long articleId, Enums.ArticleApprovalStatus approvalStatus, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<int> ChangeApprovalStatusAsync(long articleId, Enums.ArticleApprovalStatus approvalStatus, CancellationToken cancellationToken)
    {
        int rowsAffected = await database.Articles
            .Where(x => x.Id == articleId)
            .ExecuteUpdateAsync(x => x.SetProperty(x => x.ApprovalStatus, approvalStatus), cancellationToken);

        return rowsAffected;
    }
}
