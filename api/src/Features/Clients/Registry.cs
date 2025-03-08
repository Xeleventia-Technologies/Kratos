using FluentValidation;

using Kratos.Api.Common;

namespace Kratos.Api.Features.Clients;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapPost("/clients", Add.Handler.AddAsync);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.Service>();
        services.AddScoped<Add.IRepository, Add.Repository>();
    }

}
