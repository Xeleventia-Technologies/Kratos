using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class Feedback
{
    public long Id { get; set; }

    public int OutOfFiveRating { get; set; }
    public string Comment { get; set; } = null!;

    public long UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
