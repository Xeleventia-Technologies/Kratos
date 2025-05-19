using Kratos.Api.Database.Models.Identity;

namespace Kratos.Api.Database.Models;

public class Profile
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;
    public string MobileNumber { get; set; } = null!;
    public string? Bio { get; set; }
    public string? DisplayPictureFileName { get; set; }

    public long UserId { get; set; }
    public virtual User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
