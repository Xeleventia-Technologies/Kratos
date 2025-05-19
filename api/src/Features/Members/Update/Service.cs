using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Update;

public class Service(
    [FromServices] IRepository repo,
    [FromServices] IImageUploadService imageUploadService
)
{
    public async Task<Result> UpdateAsync(long memberId, Request request, CancellationToken cancellationToken)
    {
        Member? existingMember = await repo.GetByIdAsync(memberId, cancellationToken);
        if (existingMember is null)
        {
            return Result.NotFoundError("Member not found");
        }

        string? updatedDisplayPictureFileName = null;
        if (request.DisplayPicture is not null)
        {
            imageUploadService.DeleteImage(Folders.Upload.Members, existingMember.DisplayPictureFileName);
            updatedDisplayPictureFileName = await imageUploadService.UploadImageAsync(Folders.Upload.Members, request.DisplayPicture, cancellationToken);
        }

        existingMember.UpdateFrom(request);
        await repo.UpdateAsync(existingMember, updatedDisplayPictureFileName, cancellationToken);

        return Result.Success();
    }
}
