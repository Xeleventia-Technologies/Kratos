using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Extensions;
using Kratos.Api.Database.Configurations.Identity;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Auth.SignUp;

public class Service(
    [FromServices] IRepository repository,
    [FromServices] UserManager<User> userManager
)
{
    public async Task<Result> ValidateOtpAndSignUpAsync(Request request, CancellationToken cancellationToken)
    {
        User? existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
        {
            return Result.ConflictError("This email is not avaliable.");
        }

        UserOtp? userOtp = await repository.GetSignUpOtpAsync(request.Email, request.Otp, cancellationToken);
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

        await repository.DeleteSignUpOtpAsync(userOtp, cancellationToken);

        User newUser = new()
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = false
        };

        IdentityResult createUserResult = await userManager.CreateAsync(newUser, request.Password);
        if (!createUserResult.Succeeded)
        {
            return Result.CannotProcessError($"Could not process: {createUserResult.Errors.CommaSeparated()}");
        }

        newUser.EmailConfirmed = true;
        IdentityResult addInRoleResult = await userManager.AddToRoleAsync(newUser, Common.Constants.Role.User.Name);

        if (!addInRoleResult.Succeeded)
        {
            return Result.CannotProcessError($"Could not process: {addInRoleResult.Errors.CommaSeparated()}");
        }

        return Result.Success();
    }
}
