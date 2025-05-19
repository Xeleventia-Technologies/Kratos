namespace Kratos.Api.Features.Members.GetAll;

public static class Projections
{
    public record Member(long Id, string FullName, string Bio, string Position, string DisplayPictureFileName);
}
