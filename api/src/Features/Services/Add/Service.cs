using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Services.Add;

public class Service(
    IWebHostEnvironment environment,
    [FromServices] IRepository repo
)
{
    private async Task<string> UploadImageAsync(IFormFile file, CancellationToken cancellationToken)
    {
        long currentUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        string extension = Path.GetExtension(file.FileName);
        string fileName = $"{currentUnixTimestamp}{extension}";
        string uploadPath = Path.Join(environment.WebRootPath, Folders.Uploads.Services, fileName);

        using FileStream fileStream = File.OpenWrite(uploadPath);
        await file.CopyToAsync(fileStream, cancellationToken);
        
        return fileName;
    }

    public async Task<Result> AddAsync(Database.Models.Service service, IFormFile file, CancellationToken cancellationToken)
    {
        try
        {
            string fileName = await UploadImageAsync(file, cancellationToken);
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
