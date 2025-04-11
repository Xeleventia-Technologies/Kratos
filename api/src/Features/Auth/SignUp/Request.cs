namespace Kratos.Api.Features.Auth.SignUp;

public class Request
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Otp { get; set; } = null!;
}
