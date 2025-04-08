using Microsoft.AspNetCore.Identity;

namespace Kratos.Api.Database.Models.Identity;

public class User : IdentityUser<long>
{
    public virtual Profile? Profile { get; set; }
    public virtual Client? Client { get; set; }
}
