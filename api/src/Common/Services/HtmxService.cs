using Microsoft.AspNetCore.Mvc;

namespace Kratos.Api.Common.Services;

public class HtmxService([FromServices] IHttpContextAccessor httpContextAccessor)
{
    public bool IsHtmxRequest()
    {
        return httpContextAccessor.HttpContext!.Request.Headers.ContainsKey("HX-Request");
    }
}
