using Kratos.Api.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kratos.Api._Pages.Auth;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/pages/login/web", () => new RazorComponentResult<LoginWithEmailWeb>());
    }

    public void AddServices(IServiceCollection services)
    {
        
    }
}
