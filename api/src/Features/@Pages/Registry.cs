using Kratos.Api.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kratos.Api.Features.Pages;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/pages/login", () => new RazorComponentResult<Components.LoginForm>());
    }
    
    public void AddServices(IServiceCollection services)
    {
        
    }
}
