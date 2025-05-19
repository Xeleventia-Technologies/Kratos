namespace Kratos.Api.Features.Articles.Update;

public class Request
{
    public string Title { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public IFormFile? Image { get; set; }
}
