using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Testimonials.Add;

public static class Mappings
{
    public static Testimonial AsTestimonial(this Request request) => new()
    {
        Text = request.Text
    };
}
