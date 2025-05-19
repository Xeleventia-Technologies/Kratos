using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Repositories;
using Kratos.Api.Common.Types;
using Kratos.Api.Common.Extensions;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Configurations.Identity;

namespace Kratos.Api.Features.Auth.ResetPassword;

public class Service(
    [FromServices] IOtpRepository otpRepository,
    [FromServices] UserManager<User> userManager
)
{
   public async Task<Result> ResetPasswordAsync(Request request, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Result.UnauthorizedError();
        }

        UserOtp? userOtp = await otpRepository.GetOtpForEmailAsync(request.Email, request.Otp, Enums.OtpPurpose.ResetPassword, cancellationToken);
        if (userOtp is null)
        {
            return Result.CannotProcessError("Invalid or expired OTP");
        }

        DateTime currentDateTime = DateTime.UtcNow;
        DateTime expiryTime = userOtp.ExpiresAt;
        if (currentDateTime >= expiryTime)
        {
            return Result.CannotProcessError("Invalid or expired OTP");
        }

        await otpRepository.DeleteOtpAsync(userOtp, cancellationToken);

        IdentityResult removePasswordResult = await userManager.RemovePasswordAsync(user);
        if (!removePasswordResult.Succeeded)
        {
            return Result.CannotProcessError($"Failed to reset password. Reason: {removePasswordResult.Errors.CommaSeparated()}");
        }

        IdentityResult changePasswordResult = await userManager.AddPasswordAsync(user, request.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            return Result.CannotProcessError($"Failed to reset password. Reason: {changePasswordResult.Errors.CommaSeparated()}");
        }

        return Result.Success();
    }
}
