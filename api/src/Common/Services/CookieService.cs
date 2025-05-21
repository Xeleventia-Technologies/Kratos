using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using CookieOptions_ = Kratos.Api.Common.Options.CookieOptions;

namespace Kratos.Api.Common.Services;

public interface ICookieService
{
    void AppendCookie(HttpResponse httpResponse, string key, string value, string path, DateTimeOffset expires);
}

public class CookieService([FromServices] IOptions<CookieOptions_> cookieOptions) : ICookieService
{
    public void AppendCookie(HttpResponse httpResponse, string key, string value, string path, DateTimeOffset expires)
    {
        httpResponse.Cookies.Append(key, value, new CookieOptions()
        {
            Expires = expires,
            Path = path,
            Secure = cookieOptions.Value.Secure,
            HttpOnly = cookieOptions.Value.HttpOnly,
            SameSite = Enum.Parse<SameSiteMode>(cookieOptions.Value.SameSite)
        });
    }
}
