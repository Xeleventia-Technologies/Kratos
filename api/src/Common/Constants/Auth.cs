namespace Kratos.Api.Common.Constants;

public static class Auth
{
    public readonly record struct LoginProvider(string Name)
    {
        public static readonly LoginProvider Self = new("Self");
        public static readonly LoginProvider Google = new("Google");
    }

    public readonly record struct TokenType(string Name)
    {
        public static readonly TokenType AccessToken = new("AccessToken");
        public static readonly TokenType RefreshToken = new("RefreshToken");
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

    public readonly record struct Claim(string Name)
    {
        public static readonly Claim UserId = new("UserId");
        public static readonly Claim TokenType = new("TokenType");
        public static readonly Claim Permission = new("Permission");
    }
}
