using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Services;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/api/services/all", GetAll.Handler.HandleAsync);
        app.MapGet("/api/services", Get.Handler.HandleAsync);
        app.MapGet("/api/service/{serviceId}", GetById.Handler.HandleAsync);

        app.MapPost("/api/service", Add.Handler.HandleAsync)
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();

        app.MapPut("/api/service/{serviceId}", Update.Handler.HandleAsync)
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();

        app.MapDelete("/api/service/{serviceId}", Delete.Handler.HandleAsync);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<GetAll.IRepository, GetAll.Repository>();
        services.AddScoped<GetAll.Service>();

        services.AddScoped<Get.IRepository, Get.Repository>();
        services.AddScoped<Get.Service>();

        services.AddScoped<GetById.IRepository, GetById.Repository>();
        services.AddScoped<GetById.Service>();

        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.Service>();
        services.AddScoped<Add.IRepository, Add.Repository>();

        services.AddScoped<IValidator<Update.Request>, Update.RequestValidator>();
        services.AddScoped<Update.Service>();
        services.AddScoped<Update.IRepository, Update.Repository>();

        services.AddScoped<Delete.IRepository, Delete.Repository>();
        services.AddScoped<Delete.Service>();
    }
}
