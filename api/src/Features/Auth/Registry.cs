using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Auth;

public class Registry : IRegistry
{
    public const string LoginUrl = "/auth/login";
    public const string LoginWebUrl = "/auth/login/web";
    public const string GoogleLoginUrl = "/auth/google";
    public const string GoogleLoginWebUrl = "/auth/google/web";
    public const string RefreshTokensUrl = "/auth/refresh-tokens";
    public const string RefreshTokensWebUrl = "/auth/refresh-tokens/web";

    public void MapEndpoints(WebApplication app)
    {
        app.MapPost("/auth/generate-otp", GenerateOtp.Handler.HandleAsync);
        app.MapPost("/auth/sign-up", SignUp.Handler.HandleAsync);
        
        app.MapPost(LoginUrl, Login.Handler.HandleAsync);
        app.MapPost(LoginWebUrl, Login.Handler.HandleWebAsync);

        app.MapPost(GoogleLoginUrl, Google.Handler.HandleAsync);
        app.MapPost(GoogleLoginWebUrl, Google.Handler.HandleWebAsync);
        
        app.MapPost(RefreshTokensUrl, RefreshTokens.Handler.HandleAsync)
            .RequireAuthorization(Policy.AllowExpiredJwt.Name);
            
        app.MapPost(RefreshTokensWebUrl, RefreshTokens.Handler.HandleWebAsync)
            .RequireAuthorization(Policy.AllowExpiredJwt.Name);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<GenerateOtp.Request>, GenerateOtp.RequestValidator>();
        services.AddScoped<GenerateOtp.Service>();
        
        services.AddScoped<IValidator<Login.Request>, Login.RequestValidator>();
        services.AddScoped<IValidator<Login.RequestWeb>, Login.RequestWebValidator>();
        services.AddScoped<Login.Service>();

        services.AddScoped<IValidator<Google.Request>, Google.RequestValidator>();
        services.AddScoped<IValidator<Google.RequestWeb>, Google.RequestWebValidator>();
        services.AddScoped<Google.Service>();
        
        services.AddScoped<IValidator<SignUp.Request>, SignUp.RequestValidator>();
        services.AddScoped<SignUp.IRepository, SignUp.Repository>();
        services.AddScoped<SignUp.Service>();

        services.AddScoped<IValidator<RefreshTokens.Request>, RefreshTokens.RequestValidator>();
        services.AddScoped<RefreshTokens.Service>();
    }
}
