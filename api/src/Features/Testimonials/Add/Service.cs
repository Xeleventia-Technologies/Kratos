using Microsoft.AspNetCore.Mvc;

using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Testimonials.Add;

public class Service([FromServices] IRepository repo)
{
    public async Task<Result> AddAsync(Testimonial testimonial, CancellationToken cancellationToken)
    {
        if (await repo.ExistsForUser(testimonial.UserId, cancellationToken))
        {
            return Result.ConflictError("Testimonial already exists for user.");
        }

        await repo.AddAsync(testimonial, cancellationToken);
        return Result.Success();
    }
}
