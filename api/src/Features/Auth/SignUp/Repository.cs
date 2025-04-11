using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Configurations.Identity;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Auth.SignUp;

public interface IRepository
{
    Task<UserOtp?> GetSignUpOtpAsync(string email, string otp, CancellationToken cancellationToken);
    Task DeleteSignUpOtpAsync(UserOtp userOtp, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<UserOtp?> GetSignUpOtpAsync(string email, string otp, CancellationToken cancellationToken)
    {
        return await database.UserOtps.FirstOrDefaultAsync(x => x.Email == email && x.Otp == otp && x.Purpose == Enums.OtpPurpose.SignUp, cancellationToken);
    }

    public async Task DeleteSignUpOtpAsync(UserOtp userOtp, CancellationToken cancellationToken)
    {
        database.UserOtps.Remove(userOtp);
        await database.SaveChangesAsync(cancellationToken);
    }
}
