using Microsoft.AspNetCore.Identity;

namespace Kratos.Api.Database.Models.Identity;

public class User : IdentityUser<long>
{
    public DateTime CreateAt { get; set; }
    public AuthProvider AuthProvider { get; set; }

    public virtual Profile? Profile { get; set; }
    public virtual Client? Client { get; set; }
}
