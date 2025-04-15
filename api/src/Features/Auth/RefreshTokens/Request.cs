namespace Kratos.Api.Features.Auth.RefreshTokens;

public class Request
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string SessionId { get; set; } = null!;
}
