using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class Testimonial
{
    public long Id { get; set; }
    public string Text { get; set; } = null!;

    public long UserId { get; set; }
    public virtual User User { get; set; } = null!;
}
