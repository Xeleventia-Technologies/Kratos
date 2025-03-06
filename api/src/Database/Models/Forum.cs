using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class Forum
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string ImageFileName { get; set; } = null!;

    public long CreatedByUserId { get; set; }
    public virtual User CreatedByUser { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
