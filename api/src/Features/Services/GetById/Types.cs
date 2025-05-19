namespace Kratos.Api.Features.Services.GetById;

public static class Projections
{
    public record Service(
       long Id,
       string Name,
       string Summary,
       string Description,
       string ImageFileName,
       string? SeoFriendlyName,
       string? ParentServiceName,
       DateTime CreatedAt
   );

   public record ServiceForUi(
       long Id,
       string Name,
       string Summary,
       string Description,
       string ImageFileName,
       string? SeoFriendlyName,
       List<ServiceForUi>? ChildServices = null
   );
}
