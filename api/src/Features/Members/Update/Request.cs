namespace Kratos.Api.Features.Members.Update;

public class Request
{
    public string FullName { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public IFormFile? DisplayPicture { get; set; }
}
