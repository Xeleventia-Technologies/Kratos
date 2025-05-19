namespace Kratos.Api.Features.Services.Update;

public class Request
{
    public string Name { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Description { get; set; } = null!;
    public IFormFile? Image { get; set; }
    public long? ParentServiceId { get; set; }
}
