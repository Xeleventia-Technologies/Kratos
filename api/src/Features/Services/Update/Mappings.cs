namespace Kratos.Api.Features.Services.Update;

public static class Mappings
{
    public static void UpdateFrom(this Database.Models.Service service, Request request, string? imageFileName, string seoFriendlyServiceName)
    {
        service.Name = request.Name;
        service.Summary = request.Summary;
        service.Description = request.Description;
        service.ParentServiceId = request.ParentServiceId;
        service.SeoFriendlyName = seoFriendlyServiceName;

        if (imageFileName is not null)
        {
            service.ImageFileName = imageFileName;
        }
    }
}
