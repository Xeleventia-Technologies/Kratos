namespace Kratos.Api.Features.Services.Get;

public record GetParams(int? ParentId = null);

public static class Projections
{
    public record Service(
        long Id,
        string Name,
        string Summary,
        string Description,
        string ImageFileName,
        string SeoFriendlyName,
        string? ParentServiceName,
        DateTime CreatedAt
    );
}
