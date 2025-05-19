using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Clients.Update;

public class Service (
    [FromServices] UserManager<User> userManager,
    [FromServices] IRepository repo
)
{
    public async Task<Result> UpdateAsync(long clientId, Request request, CancellationToken cancellationToken)
    {   
        Client? existingClient = await repo.GetByIdAsync(clientId, cancellationToken);
        if (existingClient is null)
        {
            return Result.NotFoundError("Specified client not found");
        }

        User? user = await userManager.FindByIdAsync(existingClient.UserId.ToString());
        if (user is null)
        {
            return Result.NotFoundError("Specified user not found");
        }

        Profile? userProfile = await repo.GetProfileForUserAsync(user.Id, cancellationToken);
        if (userProfile is null)
        {
            return Result.NotFoundError("Specified user has no profile");
        }

        if (request.ResetPassword)
        {
            IdentityResult removePasswordResult = await userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
            {
                return Result.CannotProcessError($"Failed to remove password. Reason: {removePasswordResult.Errors.CommaSeparated()}");
            }

            IdentityResult addPasswordResult = await userManager.AddPasswordAsync(user, Constants.DefaultPassword);
            if (!addPasswordResult.Succeeded)
            {
                return Result.CannotProcessError($"Failed to add password. Reason: {addPasswordResult.Errors.CommaSeparated()}");
            }
        }

        await userManager.SetEmailAsync(user, request.Email);
        await userManager.SetPhoneNumberAsync(user, request.MobileNumber);

        existingClient.UpdateFrom(request);
        userProfile.UpdateFrom(request);

        await repo.UpdateAsync(existingClient, userProfile, cancellationToken);
        return Result.Success();
    }
}
