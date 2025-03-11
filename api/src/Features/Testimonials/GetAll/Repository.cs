using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Kratos.Api.Database;

namespace Kratos.Api.Features.Testimonials.GetAll;

public interface IRepository
{
    Task<List<Projections.Testimonial>> GetAllTestimonialsAsync(CancellationToken cancellationToken);
}

public class Repository([FromServices] DatabaseContext database) : IRepository
{
    public async Task<List<Projections.Testimonial>> GetAllTestimonialsAsync(CancellationToken cancellationToken)
    {
        List<Projections.Testimonial> testimonials = await database.Testimonials
            .Select(t => new Projections.Testimonial()
            {
                Text = t.Text,
                FullName = t.User.Profile!.FullName,
                DisplayPictureFileName = t.User.Profile == null ? null : t.User.Profile.DisplayPictureFileName
            })
            .ToListAsync(cancellationToken);
        
        return testimonials;
    }
}
