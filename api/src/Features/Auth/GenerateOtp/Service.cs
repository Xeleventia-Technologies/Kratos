using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Kratos.Api.Common.Services;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Auth.GenerateOtp;

public class Service(
    [FromServices] IEmailService emailService,
    [FromServices] IOtpService otpService,
    [FromServices] UserManager<User> userManager
)
{
    public async Task<Result> GenerateAndSendOptAsync(string email, Enums.OtpPurpose optPurpose, CancellationToken cancellationToken)
    {
        if (optPurpose == Enums.OtpPurpose.SignUp)
        {
            User? user = await userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                return Result.ConflictError("This email is not avaliable.");
            }
        }

        Result<string> otpResult = await otpService.GenerateAndSaveOtpAsync(email, optPurpose, cancellationToken);
        if (!otpResult.IsSuccess)
        {
            return otpResult.AsNonGeneric();
        }

        await emailService.SendEmailAsync(email, "OTP", otpResult.Value!);
        return Result.Success();
    }
}
