namespace Kratos.Api.Features.Profiles.Get;

public static class Projections
{
    public record Profile(string FullName, string MobileNumber, string? Bio, string? DisplayPictureFileName);
}
