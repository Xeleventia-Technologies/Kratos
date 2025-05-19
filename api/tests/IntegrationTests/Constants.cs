using Kratos.Api.Common.Types;

namespace Kratos.Api.Tests.IntegrationTests;

public static class Constants
{
    public static readonly GoogleUser MockGoogleUser = new("sub", "test@example.com", "Test User", "https://pic.url");

    public static class GoogleTokens
    {
        public const string CorrectToken = "fake-google-id-token";
        public const string IncorrectToken = "fake-google-id-token-incorrect";
    }

    public static class ConnectionStrings
    {
        private static string Generate(string dbName) => $"User ID=postgres;Password=root;Host=localhost;Port=5432;Database={dbName};Connection Lifetime=0;Include Error Detail=true;";

        public static string TestDbAuthGoogle => Generate("test_db_auth_google");
        public static string TestDbAuthGoogleWeb => Generate("test_db_auth_google_web");
        public static string TestDbAuthSameSessions => Generate("test_db_auth_same_sessions");
        public static string TestDbAuthMultipleSessions => Generate("test_db_auth_different_sessions");
    }
}

public static class Urls
{
    public const string GoogleLogin = "/api/auth/google";
    public const string GoogleLoginWeb = "/api/auth/google/web";
}
