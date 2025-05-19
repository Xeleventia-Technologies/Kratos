namespace Kratos.Api.Features.Profiles.Add;

public class Request
{
    public string FullName { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string? Bio { get; set; }
    public IFormFile? DisplayPicture { get; set; }
}
