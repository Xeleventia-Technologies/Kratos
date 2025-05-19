using Microsoft.AspNetCore.Mvc;

using Npgsql;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Services.Update;

public class Service(
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] IRepository repo
)
{
    public async Task<Result> UpdateAsync(long serviceId, Request request, CancellationToken cancellationToken)
    {
        Database.Models.Service? existingService = await repo.GetByIdAsync(serviceId, cancellationToken);
        if (existingService == null)
        {
            return Result.NotFoundError("Service not found");
        }

        if (request.ParentServiceId is not null)
        {
            bool parentServiceExists = await repo.ExistsAsync(request.ParentServiceId.Value, cancellationToken);
            if (!parentServiceExists)
            {
                return Result.NotFoundError("Parent service not found");
            }
        }

        string seoFriendlyServiceName = request.Name.SeoFriendly();
        bool seoFriendlyServiceNameNameExists = await repo.SeoFriendlyNameExistsAsync(existingService.Id, seoFriendlyServiceName, cancellationToken);
        if (seoFriendlyServiceNameNameExists)
        {
            return Result.ConflictError("Service name conflicts with another");
        }

        string? imageFileName = null;
        if (request.Image is not null)
        {
            bool imageDeleted = imageUploadService.DeleteImage(Folders.Upload.Services, existingService.ImageFileName);
            if (!imageDeleted)
            {
                return Result.CannotProcessError("Failed to delete existing image");
            }

            imageFileName = await imageUploadService.UploadImageAsync(Folders.Upload.Services, request.Image, cancellationToken);
        }

        existingService.UpdateFrom(request, imageFileName, seoFriendlyServiceName);

        try
        {
            await repo.UpdateAsync(existingService, cancellationToken);
            return Result.Success();
        }
        catch (PostgresException)
        {
            return Result.ConflictError("Service name conflicts with another");
        }
    }
}
