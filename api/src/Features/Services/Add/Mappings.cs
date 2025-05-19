namespace Kratos.Api.Features.Services.Add;

public static class Mappings
{
    public static Database.Models.Service AsService(this Request request, string imageFileName, string seoFriendlyServiceName) => new()
    {
        Name = request.Name,
        Summary = request.Summary,
        Description = request.Description,
        ImageFileName = imageFileName,
        SeoFriendlyName = seoFriendlyServiceName,
        ParentServiceId = request.ParentServiceId
    };
}
