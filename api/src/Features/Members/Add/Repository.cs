using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Add;

public interface IRepository
{
    Task AddAsync(Member member, string displayPictureFileName, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task AddAsync(Member member, string displayPictureFileName, CancellationToken cancellationToken)
    {
        member.DisplayPictureFileName = displayPictureFileName;
        await database.Members.AddAsync(member, cancellationToken);
        await database.SaveChangesAsync(cancellationToken);
    }
}
