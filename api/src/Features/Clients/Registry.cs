using FluentValidation;

using Kratos.Api.Common;

namespace Kratos.Api.Features.Clients;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/clients", GetAll.Handler.HandleAsync);
        app.MapGet("/client/{clientId}", GetById.Handler.HandleAsync);

        app.MapPost("/client", Add.Handler.AddAsync);

        app.MapPut("/client/{clientId}", Update.Handler.HandleAsync);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<GetAll.IRepository, GetAll.Repository>();
        services.AddScoped<GetAll.Service>();

        services.AddScoped<GetById.IRepository, GetById.Repository>();
        services.AddScoped<GetById.Service>();

        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.Service>();
        services.AddScoped<Add.IRepository, Add.Repository>();

        services.AddScoped<IValidator<Update.Request>, Update.RequestValidator>();
        services.AddScoped<Update.Service>();
        services.AddScoped<Update.IRepository, Update.Repository>();
    }

}
