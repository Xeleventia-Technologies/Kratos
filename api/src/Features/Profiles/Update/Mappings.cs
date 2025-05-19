using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Profiles.Update;

public static class Mappings
{
    public static void UpdateFrom(this Profile profile, Request request)
    {
        profile.FullName = request.FullName;
        profile.Bio = request.Bio;
        profile.MobileNumber = request.MobileNumber;
    }
}
