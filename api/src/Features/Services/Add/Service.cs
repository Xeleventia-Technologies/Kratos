using Microsoft.AspNetCore.Mvc;

using Npgsql;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Services;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Services.Add;

public class Service(
    [FromServices] IImageUploadService imageUploadService,
    [FromServices] IRepository repo
)
{
    public async Task<Result> AddAsync(Request request, CancellationToken cancellationToken)
    {
        string seoFriendlyServiceName = request.Name.SeoFriendly();

        bool serviceNameExists = await repo.ExistsAsync(seoFriendlyServiceName, cancellationToken);
        if (serviceNameExists)
        {
            return Result.ConflictError("Service name conflicts with another");
        }

        if (request.ParentServiceId is not null)
        {
            bool parentServiceExists = await repo.ExistsAsync(request.ParentServiceId.Value, cancellationToken);
            if (!parentServiceExists)
            {
                return Result.NotFoundError("Parent service not found");
            }
        }

        string imageFileName = await imageUploadService.UploadImageAsync(Folders.Upload.Services, request.Image, cancellationToken);

        try
        {
            Database.Models.Service service = request.AsService(imageFileName, seoFriendlyServiceName);
            await repo.AddServiceAsync(service, cancellationToken);

            return Result.Success();
        }
        catch (PostgresException)
        {
            return Result.ConflictError("Service name conflicts with another");
        }
    }
}
