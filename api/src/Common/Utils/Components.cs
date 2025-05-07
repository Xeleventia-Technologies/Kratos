using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Kratos.Api.Common.Utils;

public static class Components
{
    public static RazorComponentResult<TComponent> Razor<TComponent>(HttpRequest request) where TComponent : IComponent
    {
        bool isHtmx = request.Headers.ContainsKey("HX-Request");
        return new RazorComponentResult<TComponent>(new { UseLayout = !isHtmx });
    }
}
