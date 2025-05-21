namespace Kratos.Api.Database.Models;

public class CommunityThreadImage
{
    public long Id { get; set; }
    public string ImageFileName { get; set; } = null!;
    public long ThreadId { get; set; }
    public virtual CommunityThread Thread { get; set; } = null!;
}
