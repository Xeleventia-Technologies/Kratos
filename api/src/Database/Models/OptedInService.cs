namespace Kratos.Api.Database.Models;

public class OptedInService
{
    public long Id { get; set; }

    public long ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public long ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public virtual List<AssignedOptedInService> AssignedTasks { get; set; } = null!;
    public virtual List<OptedInServiceQuery> Queries { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
