using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Features.Auth.Token;

namespace Kratos.Api.Features.Auth;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapPost("/generate-otp", GenerateOtp.Handler.HandleAsync);
        app.MapPost("/signup", SignUp.Handler.HandleAsync);
        app.MapPost("/login", Login.Handler.HandleAsync);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IValidator<GenerateOtp.Request>, GenerateOtp.RequestValidator>();
        services.AddScoped<GenerateOtp.Service>();
        
        services.AddScoped<IValidator<Login.Request>, Login.RequestValidator>();
        services.AddScoped<Login.IRepository, Login.Repository>();
        services.AddScoped<Login.Service>();
        
        services.AddScoped<IValidator<SignUp.Request>, SignUp.RequestValidator>();
        services.AddScoped<SignUp.IRepository, SignUp.Repository>();
        services.AddScoped<SignUp.Service>();
    }
}
