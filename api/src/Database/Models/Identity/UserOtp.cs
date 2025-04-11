using Kratos.Api.Database.Models;

namespace Kratos.Api.Database.Configurations.Identity;

public class UserOtp
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;
    public string Otp { get; set; } = null!;
    public Enums.OtpPurpose Purpose { get; set; }
    public DateTime GeneratedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}
