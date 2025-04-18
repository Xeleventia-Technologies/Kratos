namespace Kratos.Api.Common.Utils;

public static class CookieUtils
{
    public static void AppendCookie(this HttpResponse httpResponse, string key, string value, string path, DateTimeOffset expires)
    {
        httpResponse.Cookies.Append(key, value, new CookieOptions()
        {
            Path = path,
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None,
            Expires = expires
        });
    }
}
