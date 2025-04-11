namespace Kratos.Api.Common.Options;

public class OptOptions
{
    public const string SectionName = "Otp";

    public int ExpiryInMinutes { get; set; }
    public int ResendAllowedAfterMinutes { get; set; }
}
