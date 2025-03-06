using FluentValidation;

using Kratos.Api.Common;

namespace Kratos.Api.Features.Testimonials;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapPost("/testimonials", Add.Handler.AddAsync);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.Service>();
        services.AddScoped<Add.IRepository, Add.Repository>();
    }
}
