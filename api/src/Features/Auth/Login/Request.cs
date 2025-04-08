namespace Kratos.Api.Features.Auth.Login;

public class Request
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? SessionId { get; set; }
}
