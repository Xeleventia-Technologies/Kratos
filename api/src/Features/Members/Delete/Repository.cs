using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Members.Delete;

public interface IRepository
{
    Task<Member?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task DeleteAsync(Member member, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<Member?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await database.Members
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task DeleteAsync(Member member, CancellationToken cancellationToken)
    {
        database.Members.Remove(member);
        await database.SaveChangesAsync(cancellationToken);
    }
}
