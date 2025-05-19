namespace Kratos.Api.Database.Models;

public class ForumThreadImage
{
    public long Id { get; set; }
    public string ImageFileName { get; set; } = null!;
    public long ThreadId { get; set; }
    public virtual ForumThread Thread { get; set; } = null!;
}
