using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Clients.Add;

public static class Mappings
{
    private static Client AsClient(this Request request) => new()
    {
        CloudStorageLink = request.CloudStorageLink,
    };

    private static User AsUser(this Request request) => new()
    {
        Email = request.Email,
        UserName = request.Email,
        EmailConfirmed = true,
    };

    private static Profile AsProfile(this Request request) => new()
    {
        FullName = request.FullName,
        MobileNumber = request.MobileNumber,
    };

    public static (Client client, User user, Profile profile) AsModels(this Request request) => (
        client: request.AsClient(),
        user: request.AsUser(),
        profile: request.AsProfile()
    );
}
