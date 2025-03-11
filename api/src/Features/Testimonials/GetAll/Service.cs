using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Features.Testimonials.GetAll;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result<List<Projections.Testimonial>>> GetAllTestimonialsAsync(CancellationToken cancellationToken)
    {
        List<Projections.Testimonial> testimonials = await repo.GetAllTestimonialsAsync(cancellationToken);
        return Result.Success(testimonials);
    }
}
