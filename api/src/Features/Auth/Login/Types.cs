namespace Kratos.Api.Features.Auth.Login;

public class LoginResult
{
    public required string AccessToken { get; set; } = null!;
    public required string RefreshToken { get; set; } = null!;
    public required string SessionId { get; set; } = null!;
}
