using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class Client
{
    public long Id { get; set; }

    public string CloudStorageLink { get; set; } = null!;

    public long UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
