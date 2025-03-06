namespace Kratos.Api.Features.Testimonials.Add;

public class Request
{
    public string Text { get; set; } = null!;
    public long UserId { get; set; }
}
