namespace Kratos.Api.Database.Models;

public class Member
{
    public long Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string DisplayPictureFileName { get; set; } = null!;
    public string Position { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
