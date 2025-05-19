using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Profiles.Update;

public class Service(
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] IRepository repo
)
{
    public async Task<Result> UpdateAsync(long userId, Request request, CancellationToken cancellationToken)
    {
        Profile? existingProfile = await repo.GetForUser(userId, cancellationToken);
        if (existingProfile is null)
        {
            return Result.NotFoundError("Profile not found for user");
        }

        string? displayPictureFileName = existingProfile.DisplayPictureFileName;
        if (request.DisplayPicture is not null)
        {
            if (existingProfile.DisplayPictureFileName is not null)
            {
                bool imageDeleted = imageUploadService.DeleteImage(Folders.Upload.DisplayPictures, existingProfile.DisplayPictureFileName);
                if (!imageDeleted)
                {
                    return Result.CannotProcessError("Failed to delete existing image");
                }
            }

            displayPictureFileName = await imageUploadService.UploadImageAsync(Folders.Upload.DisplayPictures, request.DisplayPicture, cancellationToken);
        }

        existingProfile.UpdateFrom(request);
        await repo.UpdateAsync(existingProfile, displayPictureFileName, cancellationToken);

        return Result.Success();
    }
}
