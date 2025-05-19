using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Profiles;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/api/profile", Get.Handler.HandleAsync)
            .RequireAuthorization();

        app.MapPost("/api/profile", Add.Handler.HandleAsync)
            .RequireAuthorization()
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();;

        app.MapPut("/api/profile", Update.Handler.HandleAsync)
            .RequireAuthorization()
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();;
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<Get.IRepository, Get.Repository>();
        services.AddScoped<Get.Service>();

        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.IRepository, Add.Repository>();
        services.AddScoped<Add.Service>();

        services.AddScoped<IValidator<Update.Request>, Update.RequestValidator>();
        services.AddScoped<Update.IRepository, Update.Repository>();
        services.AddScoped<Update.Service>();
    }
}
