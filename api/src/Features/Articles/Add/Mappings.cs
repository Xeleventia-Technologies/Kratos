using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.Add;

public static class Mappings
{
    public static Article AsArticle(this Request request, long userId, Enums.ArticleApprovalStatus approvalStatus, string? imageFileName) => new()
    {
        Title = request.Title,
        Summary = request.Summary,
        Content = request.Content,
        ImageFileName = imageFileName,
        CreatedByUserId = userId,
        ApprovalStatus = approvalStatus,
    };
}
