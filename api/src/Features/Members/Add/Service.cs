using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Constants;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Add;

public class Service(
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] IRepository repo
)
{
    public async Task<Result> AddAsync(Member member, IFormFile displayPictureFile, CancellationToken cancellationToken)
    {
        try
        {
            string fileName = await imageUploadService.UploadImageAsync(Folders.Upload.Members, displayPictureFile, cancellationToken);
            await repo.AddAsync(member, fileName, cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.BadRequestError("Failed to add service. Reason: " + ex.Message);
        }
    }
}
