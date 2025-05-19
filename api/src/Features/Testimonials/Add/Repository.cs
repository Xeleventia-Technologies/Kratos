using Kratos.Api.Database;
using Kratos.Api.Database.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kratos.Api.Features.Testimonials.Add;

public interface IRepository
{
    Task<bool> ExistsForUser(long userId, CancellationToken cancellationToken);
    Task AddAsync(long userId, Testimonial testimonial, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> ExistsForUser(long userId, CancellationToken cancellationToken)
    {
        return await database.Testimonials.AnyAsync(x => x.UserId == userId, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(long userId, Testimonial testimonial, CancellationToken cancellationToken)
    {
        testimonial.UserId = userId;
        database.Testimonials.Add(testimonial);

        await database.SaveChangesAsync(cancellationToken);
    }
}
