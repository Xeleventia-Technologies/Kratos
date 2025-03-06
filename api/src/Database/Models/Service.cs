namespace Kratos.Api.Database.Models;

public class Service
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageFileName { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
