using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Profiles.Add;

public class Service(
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] IRepository repo
)
{
    public async Task<Result> AddAsync(long userId, Profile profile, IFormFile? displayPicture, CancellationToken cancellationToken)
    {
        bool profileExists = await repo.ExistsForUser(userId, cancellationToken);
        if (profileExists)
        {
            return Result.ConflictError("Profile already exists for user");
        }

        string? displayPictureFileName = (displayPicture is not null) 
            ? await imageUploadService.UploadImageAsync(Folders.Upload.DisplayPictures, displayPicture, cancellationToken) 
            : null;

        await repo.AddAsync(profile, userId, displayPictureFileName, cancellationToken);
        return Result.Success(SuccessStatus.Created);
    }
}
