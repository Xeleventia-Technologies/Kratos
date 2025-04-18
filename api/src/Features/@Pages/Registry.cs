using Microsoft.AspNetCore.Http.HttpResults;

using Kratos.Api.Common;

namespace Kratos.Api.Features.Pages;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/pages/login", () => new RazorComponentResult<Components.LoginWithEmail>());
        app.MapGet("/pages/login/web", () => new RazorComponentResult<Components.LoginWithEmailWeb>());

        app.MapGet("/pages/login/google", () => new RazorComponentResult<Components.LoginWithGoogle>());
        app.MapGet("/pages/login/google/web", () => new RazorComponentResult<Components.LoginWithGoogleWeb>());
    }
    
    public void AddServices(IServiceCollection services)
    {
        
    }
}
