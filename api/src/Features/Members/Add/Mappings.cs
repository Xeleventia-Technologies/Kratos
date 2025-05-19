using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Add;

public static class Mappings
{
    public static Member AsMember(this Request request) => new()
    {
        FullName = request.FullName,
        Bio = request.Bio,
        Position = request.Position,
    };
}
