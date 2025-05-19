namespace Kratos.Api.Features.Auth.UpdatePassword;

public class Request
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
