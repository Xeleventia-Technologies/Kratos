using Kratos.Api.Common.Types;
using Microsoft.AspNetCore.Mvc;

namespace Kratos.Api.Features.Testimonials.Delete;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> DeleteTestimonialForUserAsync(long userId, CancellationToken cancellationToken)
    {
        bool testimonialExists = await repo.TestimonialExistsForUserAsync(userId, cancellationToken);
        if (!testimonialExists)
        {
            return Result.NotFoundError("Testimonial for the specified user does not exist");
        }

        await repo.DeleteTestimonialForUserAsync(userId, cancellationToken);
        return Result.Success();
    }
}
