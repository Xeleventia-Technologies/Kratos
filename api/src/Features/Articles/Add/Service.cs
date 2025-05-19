using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Ganss.Xss;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Articles.Add;

public class Service(
    [FromServices] IRepository repo,
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] UserManager<User> userManager
)
{
    private readonly HtmlSanitizer htmlSanitizer = new();

    public async Task<Result> AddAsync(long userId, Request request, CancellationToken cancellationToken)
    {
        request.Content = htmlSanitizer.Sanitize(request.Content);

        Enums.ArticleApprovalStatus articleApprovalStatus = await GetDefaultApprovalStatusAsync(userId, request, cancellationToken);;

        string? uploadedImageFileName = null;
        if (request.Image is not null)
        {
            uploadedImageFileName = await imageUploadService.UploadImageAsync(Folders.Upload.Artciles, request.Image, cancellationToken);
        }

        Article article = request.AsArticle(userId, articleApprovalStatus, uploadedImageFileName);

        await repo.AddAsync(userId, article, cancellationToken);
        return Result.Success(SuccessStatus.Created);
    }

    private async Task<Enums.ArticleApprovalStatus> GetDefaultApprovalStatusAsync(long userId, Request request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByIdAsync(userId.ToString());
        IList<string> userRoles = await userManager.GetRolesAsync(user!);
        
        Enums.ArticleApprovalStatus articleApprovalStatus = userRoles[0] == Common.Constants.Role.Admin.Name
            ? Enums.ArticleApprovalStatus.Approved
            : Enums.ArticleApprovalStatus.Pending;

        return articleApprovalStatus;
    }
}
