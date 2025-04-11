using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Configurations.Identity;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Common.Repositories;

public interface IOtpRepository
{
    Task<UserOtp?> GetOtpForEmailAsync(string email, Enums.OtpPurpose purpose, CancellationToken cancellationToken);
    Task<UserOtp?> GetOtpForEmailAsync(string email, string otp, Enums.OtpPurpose purpose, CancellationToken cancellationToken);
    Task AddOrUpdateOtpAsync(UserOtp userOtp, CancellationToken cancellationToken);
    Task DeleteOtpAsync(UserOtp userOtp, CancellationToken cancellationToken);
}

public class OtpRepository([FromServices] DatabaseContext database) : IOtpRepository
{
    public async Task<UserOtp?> GetOtpForEmailAsync(string email, Enums.OtpPurpose purpose, CancellationToken cancellationToken)
    {
        return await database.UserOtps.FirstOrDefaultAsync(x => x.Email == email && x.Purpose == purpose, cancellationToken);
    }

    public async Task<UserOtp?> GetOtpForEmailAsync(string email, string otp, Enums.OtpPurpose purpose, CancellationToken cancellationToken)
    {
        return await database.UserOtps.FirstOrDefaultAsync(x => x.Email == email && x.Otp == otp && x.Purpose == purpose, cancellationToken);
    }

    public async Task AddOrUpdateOtpAsync(UserOtp userOtp, CancellationToken cancellationToken)
    {
        database.UserOtps.Update(userOtp);
        await database.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOtpAsync(UserOtp userOtp, CancellationToken cancellationToken)
    {
        database.UserOtps.Remove(userOtp);
        await database.SaveChangesAsync(cancellationToken);
    }
}
