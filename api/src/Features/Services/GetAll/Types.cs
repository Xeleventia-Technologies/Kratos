namespace Kratos.Api.Features.Services.GetAll;

public record GetParams(int? ParentId = null, bool? All = null);

public static class Projections
{
    public record Service(
        long Id,
        string Name,
        string Summary,
        string Description,
        string ImageFileName,
        string SeoFriendlyName,
        long? ParentServiceId,
        string? ParentServiceName,
        DateTime CreatedAt
    );
}
