namespace Kratos.Api.Features.Clients.Add;

public class Request
{
    public string CloudStorageLink { get; set; } = null!;
    public long UserId { get; set; }
}