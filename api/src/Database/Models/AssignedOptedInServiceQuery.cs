using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class AssignedOptedInServiceQuery
{
    public long Id { get; set; }
    public string Question { get; set; } = null!;
    public string Answer { get; set; } = null!;

    public long? AnsweredByUserId { get; set; }
    public virtual User? AnsweredByUser { get; set; }

    public long AssignedOptedInServiceId { get; set; }
    public virtual AssignedOptedInService AssignedOptedInService { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
