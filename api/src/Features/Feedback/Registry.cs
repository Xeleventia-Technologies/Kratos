using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Feeback;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/api/feedbacks", GetAll.Handler.HandleAsync);

        app.MapPost("/api/feedback", Add.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwtUser.Name);

        app.MapPut("/api/feedback", Update.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwtUser.Name);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<GetAll.IRepository, GetAll.Repository>();
        services.AddScoped<GetAll.Service>();

        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.IRepository, Add.Repository>();
        services.AddScoped<Add.Service>();

        services.AddScoped<IValidator<Update.Request>, Update.RequestValidator>();
        services.AddScoped<Update.IRepository, Update.Repository>();
        services.AddScoped<Update.Service>();
    }
}
