namespace Kratos.Api.Features.Auth.GenerateOtp;

public class Request
{
    public string Email { get; set; } = null!;
    public string Purpose { get; set; } = null!;
}
