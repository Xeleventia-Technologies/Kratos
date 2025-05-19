using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Auth;

public class Registry : IRegistry
{
    public const string LoginUrl = "/api/auth/login";
    public const string LoginWebUrl = "/api/auth/login/web";
    public const string GoogleLoginUrl = "/api/auth/google";
    public const string GoogleLoginWebUrl = "/api/auth/google/web";
    public const string RefreshTokensUrl = "/api/auth/refresh-tokens";
    public const string RefreshTokensWebUrl = "/api/auth/refresh-tokens/web";

    public void MapEndpoints(WebApplication app)
    {
        app.MapPost("/api/auth/generate-otp", GenerateOtp.Handler.HandleAsync);
        app.MapPost("/api/auth/sign-up", SignUp.Handler.HandleAsync);

        app.MapPost(LoginUrl, Login.Handler.HandleAsync);
        app.MapPost(LoginWebUrl, Login.Handler.HandleWebAsync);

        app.MapPost(GoogleLoginUrl, Google.Handler.HandleAsync);
        app.MapPost(GoogleLoginWebUrl, Google.Handler.HandleWebAsync);

        app.MapPost(RefreshTokensUrl, RefreshTokens.Handler.HandleAsync)
            .RequireAuthorization(Policy.AllowExpiredJwt.Name);

        app.MapPost(RefreshTokensWebUrl, RefreshTokens.Handler.HandleWebAsync)
            .RequireAuthorization(Policy.AllowExpiredJwt.Name);

        app.MapPost("/api/auth/update-password", UpdatePassword.Handler.HandleAsync)
            .RequireAuthorization();

        app.MapPost("/api/auth/reset-password", ResetPassword.Handler.HandleAsync)
            .RequireAuthorization();

        app.MapDelete("/api/auth/logout", Logout.Handler.HandleAsync)
            .RequireAuthorization();

        app.MapDelete("/api/auth/logout/web", Logout.Handler.HandleWebAsync)
            .RequireAuthorization();

        app.MapDelete("/api/auth/logout-all", LogoutAll.Handler.HandleAsync)
            .RequireAuthorization();

        app.MapDelete("/api/auth/logout-all/web", LogoutAll.Handler.HandleWebAsync)
            .RequireAuthorization();
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<GenerateOtp.Request>, GenerateOtp.RequestValidator>();
        services.AddScoped<GenerateOtp.Service>();

        services.AddScoped<IValidator<SignUp.Request>, SignUp.RequestValidator>();
        services.AddScoped<SignUp.Service>();

        services.AddScoped<IValidator<Login.Request>, Login.RequestValidator>();
        services.AddScoped<IValidator<Login.RequestWeb>, Login.RequestWebValidator>();
        services.AddScoped<Login.Service>();

        services.AddScoped<IValidator<Google.Request>, Google.RequestValidator>();
        services.AddScoped<IValidator<Google.RequestWeb>, Google.RequestWebValidator>();
        services.AddScoped<Google.Service>();

        services.AddScoped<IValidator<RefreshTokens.Request>, RefreshTokens.RequestValidator>();
        services.AddScoped<RefreshTokens.Service>();
        
        services.AddScoped<IValidator<UpdatePassword.Request>, UpdatePassword.RequestValidator>();
        services.AddScoped<UpdatePassword.Service>();
        
        services.AddScoped<IValidator<ResetPassword.Request>, ResetPassword.RequestValidator>();
        services.AddScoped<ResetPassword.Service>();
        
        services.AddScoped<IValidator<Logout.Request>, Logout.RequestValidator>();
        services.AddScoped<Logout.IRepository, Logout.Repository>();
        services.AddScoped<Logout.Service>();

        services.AddScoped<LogoutAll.IRepository, LogoutAll.Repository>();
        services.AddScoped<LogoutAll.Service>();
    }
}
