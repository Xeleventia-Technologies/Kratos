namespace Kratos.WebApi.Common.Constants;

public static class Auth
{
    public static class TokenTypes
    {
        public static readonly string AccessToken = "AccessToken";
        public static readonly string RefreshToken = "RefreshToken";
    }

    public static class Schemes
    {
        public static readonly string ValidJwt = "ValidJwtScheme";
        public static readonly string ExpiredJwt = "ExpiredJwtScheme";

    }

    public static class Roles
    {
        public static readonly string Admin = "Admin";
        public static readonly string User = "User";
    }

    public static class Policies
    {
        public static readonly string RequireValidJwt = "RequireValidJwt";
        public static readonly string AllowExpiredJwt = "AllowExpiredJwt";

        public static readonly string RequireAdmin = "RequireAdmin";
        public static readonly string RequireUser = "User";
    }

    public static class Claims
    {
        public static readonly string UserId = "UserId";
        public static readonly string TokenType = "TokenType";
    }
}
