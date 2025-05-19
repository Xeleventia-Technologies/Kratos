namespace Kratos.Api.Features.Feeback.GetAll;

public static class Projections
{
    public record Feedback(long Id , string FullName, int OutOfFiveRating, string? Comment);
}
