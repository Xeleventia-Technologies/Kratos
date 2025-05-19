using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class OptedInServiceQuery
{
    public long Id { get; set; }
    public string Question { get; set; } = null!;
    public string? Answer { get; set; }

    public long? AnsweredByUserId { get; set; }
    public virtual User? AnsweredByUser { get; set; }

    public long OptedInServiceId { get; set; }
    public virtual OptedInService OptedInService { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
