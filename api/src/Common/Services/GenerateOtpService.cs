namespace Kratos.Api.Common.Services;

public interface IGenerateOtpService
{
    string GenerateOtpAsync();
}

public class GenerateOtpService : IGenerateOtpService
{
    public string GenerateOtpAsync()
    {
        long otp = Random.Shared.NextInt64(0, 999999);
        return otp.ToString().PadLeft(6, '0');
    }
}
