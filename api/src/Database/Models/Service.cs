namespace Kratos.Api.Database.Models;

public class Service
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageFileName { get; set; } = null!;
    public string SeoFriendlyName { get; set; } = null!;

    public long? ParentServiceId { get; set; }
    public virtual Service? ParentService { get; set; }

    public List<Service> ChildServices { get; set; } = null!;

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
