namespace Kratos.Api.Common.Options;

public class EmailOptions
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string SmtpHost { get; init; } = null!;
    public int SmtpPort { get; init; }
}
