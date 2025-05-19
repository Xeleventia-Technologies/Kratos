namespace Kratos.Api.Features.Clients.Add;

public class Request
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string CloudStorageLink { get; set; } = null!;
}
