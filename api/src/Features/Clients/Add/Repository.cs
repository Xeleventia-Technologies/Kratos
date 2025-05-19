using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Clients.Add;

public interface IRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);
    Task<bool> MobileNumberExistsAsync(string mobileNumber, CancellationToken cancellationToken);
    Task AddAsync(User user, Profile profile, Client client, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return await database.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> MobileNumberExistsAsync(string mobileNumber, CancellationToken cancellationToken)
    {
        return await database.Profiles.AnyAsync(x => x.MobileNumber == mobileNumber, cancellationToken);
    }

    public async Task<bool> ExistsForUser(long UserId, CancellationToken cancellationToken)
    {
        return await database.Clients.AnyAsync(x => x.UserId == UserId, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(User user, Profile profile, Client client, CancellationToken cancellationToken)
    {
        client.User = user;
        profile.User = user;

        await database.Profiles.AddAsync(profile, cancellationToken);
        await database.Clients.AddAsync(client, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);
    }
}
