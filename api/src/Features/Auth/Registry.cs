using FluentValidation;

using Kratos.Api.Common;

namespace Kratos.Api.Features.Auth;

public class Registry : IRegistry
{
    public const string LoginUrl = "/login";
    public const string LoginWebUrl = "/login/web";
    public const string RefreshTokensUrl = "/refresh-tokens";
    public const string RefreshTokensWebUrl = "/refresh-tokens/web";

    public void MapEndpoints(WebApplication app)
    {
        app.MapPost("/generate-otp", GenerateOtp.Handler.HandleAsync);
        app.MapPost("/signup", SignUp.Handler.HandleAsync);
        
        app.MapPost(LoginUrl, Login.Handler.HandleAsync);
        app.MapPost(LoginWebUrl, Login.Handler.HandleWebAsync);
        
        app.MapPost(RefreshTokensUrl, RefreshTokens.Handler.HandleAsync);
        app.MapPost(RefreshTokensWebUrl, RefreshTokens.Handler.HandleWebAsync);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<GenerateOtp.Request>, GenerateOtp.RequestValidator>();
        services.AddScoped<GenerateOtp.Service>();
        
        services.AddScoped<IValidator<Login.Request>, Login.RequestValidator>();
        services.AddScoped<Login.IRepository, Login.Repository>();
        services.AddScoped<Login.Service>();
        
        services.AddScoped<IValidator<SignUp.Request>, SignUp.RequestValidator>();
        services.AddScoped<SignUp.IRepository, SignUp.Repository>();
        services.AddScoped<SignUp.Service>();

        services.AddScoped<IValidator<RefreshTokens.Request>, RefreshTokens.RequestValidator>();
        services.AddScoped<RefreshTokens.IRepository, RefreshTokens.Repository>();
        services.AddScoped<RefreshTokens.Service>();
    }
}
