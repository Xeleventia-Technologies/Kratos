using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class AssignedOptedInService
{
    public long Id { get; set; }
    public string Description { get; set; } = null!;
    public Enums.AssignedOptedInServiceStatus Status { get; set; }

    public long OptedInServiceId { get; set; }
    public virtual OptedInService OptedInService { get; set; } = null!;

    public long AssignedToUserId { get; set; }
    public virtual User AssignedToUser { get; set; } = null!;

    public long AssignedByUserId { get; set; }
    public virtual User AssignedByUser { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
