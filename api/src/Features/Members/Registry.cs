using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Members;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/api/members", GetAll.Handler.HandleAsync);

        app.MapPost("/api/member", Add.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwtAdmin.Name)
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();

        app.MapPut("/api/member/{memberId}", Update.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwtAdmin.Name)
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();

        app.MapDelete("/api/member/{memberId}", Delete.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwtAdmin.Name);
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

        services.AddScoped<Delete.IRepository, Delete.Repository>();
        services.AddScoped<Delete.Service>();
    }
}
