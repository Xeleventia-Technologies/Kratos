using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Members.Delete;

public class Service(
    [FromServices] IRepository repo,
    [FromServices] IImageUploadService imageUploadService
)
{
    public async Task<Result> DeleteAsync(long memberId, CancellationToken cancellationToken)
    {
        Member? member = await repo.GetByIdAsync(memberId, cancellationToken);
        if (member is null)
        {
            return Result.NotFoundError("Specified member not found");
        }

        imageUploadService.DeleteImage(Folders.Upload.Members, member.DisplayPictureFileName);
        await repo.DeleteAsync(member, cancellationToken);

        return Result.Success();
    }
}
