namespace Kratos.Api.Features.Auth.Login;

public class GeneratedTokens
{
    public required string AccessToken { get; set; } = null!;
    public required string RefreshToken { get; set; } = null!;
    public required string SessionId { get; set; } = null!;
}

public record AccessTokenResponse(string AccessToken);
