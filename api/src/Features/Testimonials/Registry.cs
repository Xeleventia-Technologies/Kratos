using FluentValidation;

using Kratos.Api.Common;

namespace Kratos.Api.Features.Testimonials;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/testimonials", GetAll.Handler.HandleAsync);
        app.MapPost("/testimonial", Add.Handler.HandleAsync);
        app.MapDelete("/testimonial/user/{userId}", Delete.Handler.HandleAsync);
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
