namespace Kratos.Api.Database.Models.Identity;

public class UserToken
{
    public long Id { get; set; }
    public TokenProvider Provider { get; set; }
    public string RefreshToken { get; set; } = null!;
    public string FirebaseToken { get; set; } = null!;

    public long UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public DateTime IssuedOnDateTime { get; set; }
    public DateTime? ExpiresOnDateTIme { get; set; }
}
