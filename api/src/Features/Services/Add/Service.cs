using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;

namespace Kratos.Api.Features.Services.Add;

public class Service(
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] IRepository repo
)
{
    public async Task<Result> AddAsync(Database.Models.Service service, IFormFile file, CancellationToken cancellationToken)
    {
        try
        {
            string fileName = await imageUploadService.UploadImageAsync(Folders.Uploads.Services, file, cancellationToken);
            service.ImageFileName = fileName;

            await repo.AddServiceAsync(service, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.BadRequestError("Failed to add service. Reason: " + ex.Message);
        }
    }
}
