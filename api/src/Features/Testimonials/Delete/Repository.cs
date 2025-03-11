using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace Kratos.Api.Features.Testimonials.Delete;

public interface IRepository
{
    Task<bool> TestimonialExistsForUserAsync(long userId, CancellationToken cancellationToken);
    Task<bool> DeleteTestimonialForUserAsync(long userId, CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<bool> TestimonialExistsForUserAsync(long userId, CancellationToken cancellationToken)
    {
        return await database.Testimonials.AnyAsync(t => t.UserId == userId, cancellationToken);
    }

    public async Task<bool> DeleteTestimonialForUserAsync(long userId, CancellationToken cancellationToken)
    {
        int rowsAffected = await database.Testimonials
            .Where(t => t.UserId == userId)
            .ExecuteDeleteAsync(cancellationToken);

        return rowsAffected >= 1;
    }
}
