using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Services.Update;

public class Service(
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] IRepository repo
)
{
    public async Task<Result> UpdateAsync(long serviceId, Database.Models.Service newService, IFormFile? newImage, CancellationToken cancellationToken)
    {
        Database.Models.Service? existingService = await repo.GetByIdAsync(serviceId, cancellationToken);
        if (existingService == null)
        {
            return Result.NotFoundError("Service not found");
        }

        if (newImage is not null)
        {
            bool imageDeleted = imageUploadService.DeleteImage(Folders.Uploads.Services, existingService.ImageFileName);
            if (!imageDeleted)
            {
                return Result.CannotProcessError("Failed to delete existing image");
            }

            string newImageName = await imageUploadService.UploadImageAsync(Folders.Uploads.Services, newImage, cancellationToken);
            newService.ImageFileName = newImageName;
        }
        
        await repo.UpdateAsync(existingService, newService, cancellationToken);
        return Result.Success();
    }
}
