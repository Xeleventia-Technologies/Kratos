namespace Kratos.Api.Features.Testimonials.GetAll;

public static class Projections
{
    public record Testimonial(long Id, string Text, string FullName, string? DisplayPictureFileName);
}
