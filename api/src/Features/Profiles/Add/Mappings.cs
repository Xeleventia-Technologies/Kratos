using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Profiles.Add;

public static class Mappings
{
    public static Profile AsProfile(this Request request) => new()
    {
        FullName = request.FullName,
        Bio = request.Bio,
        MobileNumber = request.MobileNumber,
    };
}
