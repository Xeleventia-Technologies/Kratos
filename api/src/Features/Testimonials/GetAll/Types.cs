namespace Kratos.Api.Features.Testimonials.GetAll;

public static class Projections
{
    public class Testimonial
    {
        public string Text { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? DisplayPictureFileName { get; set; }
    }
}
