using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Update;

public interface IRepository
{
    Task<Member?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task UpdateAsync(Member member, string? displayPictureFileName, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Member?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await database.Members
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Member member, string? displayPictureFileName, CancellationToken cancellationToken)
    {
        if (displayPictureFileName is not null)
        {
            member.DisplayPictureFileName = displayPictureFileName;
        }

        database.Members.Update(member);
        await database.SaveChangesAsync(cancellationToken);
    }
}
