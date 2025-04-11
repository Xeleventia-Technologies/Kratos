using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Kratos.Api.Common.Options;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Repositories;
using Kratos.Api.Database.Configurations.Identity;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Common.Services;

public interface IOtpService
{
    Task<Result<string>> GenerateAndSaveOtpAsync(string email, Enums.OtpPurpose purpose, CancellationToken cancellationToken);
}

public class OtpService(
    [FromServices] IOtpRepository otpRepository,
    [FromServices] IOptions<OptOptions> otpOptions
) : IOtpService
{
    public async Task<Result<string>> GenerateAndSaveOtpAsync(string email, Enums.OtpPurpose purpose, CancellationToken cancellationToken)
    {
        UserOtp? userOtp = await otpRepository.GetOtpForEmailAsync(email, purpose, cancellationToken);
        string otp;
        
        if (userOtp is null)
        {
            otp = await GenerateAndSaveOtpAsync_(email, purpose, cancellationToken);
            return Result.Success(otp);
        }

        bool shouldRegenreate = ShouldRegenreate(userOtp.GeneratedAt, otpOptions.Value.ExpiryInMinutes);
        if (!shouldRegenreate)
        {
            return Result.CannotProcessError($"OTP was already generated less than {otpOptions.Value.ResendAllowedAfterMinutes} minute(s) ago.");
        }

        if (shouldRegenreate)
        {
            await otpRepository.DeleteOtpAsync(userOtp, cancellationToken);
        }

        otp = await GenerateAndSaveOtpAsync_(email, purpose, cancellationToken);
        return Result.Success(otp);
    }

    private async Task<string> GenerateAndSaveOtpAsync_(string email, Enums.OtpPurpose purpose, CancellationToken cancellationToken)
    {
        string otp = Random.Shared.NextInt64(0, 999999).ToString().PadLeft(6, '0');
        int expiryInMinutes = otpOptions.Value.ExpiryInMinutes;

        DateTime generatedAt = DateTime.UtcNow;
        DateTime expiresAt = generatedAt.AddMinutes(expiryInMinutes);

        UserOtp userOtp = new()
        {
            Email = email,
            Otp = otp,
            Purpose = purpose,
            GeneratedAt = generatedAt,
            ExpiresAt = expiresAt,
        };

        await otpRepository.AddOrUpdateOtpAsync(userOtp, cancellationToken);
        return userOtp.Otp;
    }

    private static bool ShouldRegenreate(DateTime generatedAt, int expiryInMinutes)
    {
        DateTime currentDateTime = DateTime.UtcNow;
        DateTime resendAllowedAfterDateTime = generatedAt.AddMicroseconds(expiryInMinutes);

        return currentDateTime >= resendAllowedAfterDateTime;
    }
}