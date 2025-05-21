using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Articles.Update;

public class Service(
    [FromServices] IRepository repo,
    [FromServices] IImageUploadService imageUploadService
)
{
    public async Task<Result> UpdateAsync(long userId, long articleId, Request request, CancellationToken cancellationToken)
    {
        Article? existingArticle = await repo.GetByIdAsync(articleId, cancellationToken);
        if (existingArticle is null)
        {
            return Result.NotFoundError("Article not found");
        }

        if (existingArticle.CreatedByUserId != userId)
        {
            return Result.ForbiddenError("Cannot update this article");
        }

        string? uploadedImageFileName = null;
        if (request.Image is not null)
        {
            if (existingArticle.ImageFileName is not null)
            {
                imageUploadService.DeleteImage(Folders.Upload.Artciles, existingArticle.ImageFileName);
            }
            uploadedImageFileName = await imageUploadService.UploadImageAsync(Folders.Upload.Artciles, request.Image, cancellationToken);
        }

        existingArticle.UpdateFrom(request, uploadedImageFileName);
        await repo.UpdateAsync(existingArticle, cancellationToken);
        
        return Result.Success();
    }
}
