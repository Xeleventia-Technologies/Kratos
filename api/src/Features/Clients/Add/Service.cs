using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Clients.Add;

public class Service(
    [FromServices] UserManager<User> userManager,
    [FromServices] IRepository repo
)
{
    public async Task<Result> AddAsync(Request request, CancellationToken cancellationToken)
    {
        bool emailExists = await repo.EmailExistsAsync(request.Email, cancellationToken);
        if (emailExists)
        {
            return Result.ConflictError("This email is not avaliable.");
        }

        bool mobileExists = await repo.MobileNumberExistsAsync(request.MobileNumber, cancellationToken);
        if (mobileExists)
        {
            return Result.ConflictError("This mobile number is not avaliable.");
        }

        (Client client, User user, Profile profile) = request.AsModels();

        IdentityResult result = await userManager.CreateAsync(user, Constants.DefaultPassword);
        if (!result.Succeeded)
        {
            return Result.CannotProcessError($"Failed to create user. Reason: {result.Errors.CommaSeparated()}");
        }

        await repo.AddAsync(user, profile, client, cancellationToken);
        return Result.Success();
    }
}
