using Microsoft.AspNetCore.Http.HttpResults;

using Kratos.Api.Common;
using Kratos.Api._Pages.Auth;
using Kratos.Api.Common.Utils;

namespace Kratos.Api._Pages;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/pages/login", () => new RazorComponentResult<LoginWithEmail>());
        app.MapGet("/pages/login/web", () => new RazorComponentResult<LoginWithEmailWeb>());

        app.MapGet("/pages/login/google", () => new RazorComponentResult<LoginWithGoogle>());
        app.MapGet("/pages/login/google/web", () => new RazorComponentResult<LoginWithGoogleWeb>());
    }
    
    public void AddServices(IServiceCollection services) { }
}
