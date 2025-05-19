using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Testimonials;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/api/testimonials", GetAll.Handler.HandleAsync);
        
        app.MapPost("/api/testimonial", Add.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwtUser.Name);

        app.MapDelete("/api/testimonial/user/{userId}", Delete.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwtUser.Name);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<GetAll.IRepository, GetAll.Repository>();
        services.AddScoped<GetAll.Service>();

        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.IRepository, Add.Repository>();
        services.AddScoped<Add.Service>();

        services.AddScoped<Delete.IRepository, Delete.Repository>();
        services.AddScoped<Delete.Service>();
    }
}
