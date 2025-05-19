using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Features.Clients.Update;

public static class Mappings
{
    public static void UpdateFrom(this Client client, Request request)
    {
        client.CloudStorageLink = request.CloudStorageLink;
    }

    public static void UpdateFrom(this User user, Request request)
    {
        user.Email = request.Email;
        user.UserName = request.Email;
    }

    public static void UpdateFrom(this Profile profile, Request request)
    {
        profile.FullName = request.FullName;
        profile.MobileNumber = request.MobileNumber;
    }
}
