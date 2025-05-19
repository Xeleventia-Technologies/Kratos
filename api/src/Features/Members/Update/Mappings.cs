using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Update;

public static class Mappings
{
    public static void UpdateFrom(this Member member, Request request, string? displayPictureFileName)
    {
        member.FullName = request.FullName;
        member.Bio = request.Bio;
        member.Position = request.Position;
        
        if (displayPictureFileName is not null)
        {
            member.DisplayPictureFileName = displayPictureFileName;
        }
    }
}
