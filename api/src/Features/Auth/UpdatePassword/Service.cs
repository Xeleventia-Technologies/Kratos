using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Common.Extensions;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Auth.UpdatePassword;

public class Service([FromServices] UserManager<User> userManager)
{
    public async Task<Result> UpdatePasswordAsync(long userId, Request request)
    {
        User? user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
        {
            return Result.UnauthorizedError();
        }

        IdentityResult changePasswordResult = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            return Result.CannotProcessError($"Failed to change password. Reason: {changePasswordResult.Errors.CommaSeparated()}");
        }

        return Result.Success();
    }
}
