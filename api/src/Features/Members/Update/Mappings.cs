using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Update;

public static class Mappings
{
    public static void UpdateFrom(this Member member, Request request)
    {
        member.FullName = request.FullName;
        member.Bio = request.Bio;
    }
}
