namespace Kratos.Api.Features.Auth.ResetPassword;

public class Request
{
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string Otp { get; set; } = null!;
}
