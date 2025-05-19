using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Articles;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapGet("/api/articles", GetAll.Handler.HandelAsync);
        
        app.MapGet("/api/articles/my", GetForUser.Handler.HandelAsync)
            .RequireAuthorization();

        app.MapGet("/api/articles/approved", GetApproved.Handler.HandelAsync);
        app.MapGet("/api/article/{articleId}", GetById.Handler.HandleAsync);

        app.MapPost("/api/article", Add.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwt.Name)
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();

        app.MapPut("/api/article/{articleId}", Update.Handler.HandleAsync)
            .RequireAuthorization(Policy.RequireValidJwt.Name)
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();

        app.MapPatch("/api/article/{articleId}/change-approval-status", ChangeApprovalStatus.Handler.HandleAsync);
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<GetAll.FilterGetParams>, GetAll.ParamsValidator>();
        services.AddScoped<GetAll.IRepository, GetAll.Repository>();
        services.AddScoped<GetAll.Service>();

        services.AddScoped<IValidator<GetForUser.FilterGetParams>, GetForUser.ParamsValidator>();
        services.AddScoped<GetForUser.IRepository, GetForUser.Repository>();
        services.AddScoped<GetForUser.Service>();

        services.AddScoped<IValidator<GetApproved.FilterGetParams>, GetApproved.ParamsValidator>();
        services.AddScoped<GetApproved.IRepository, GetApproved.Repository>();
        services.AddScoped<GetApproved.Service>();

        services.AddScoped<GetById.IRepository, GetById.Repository>();
        services.AddScoped<GetById.Service>();

        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.IRepository, Add.Repository>();
        services.AddScoped<Add.Service>();

        services.AddScoped<IValidator<ChangeApprovalStatus.Request>, ChangeApprovalStatus.RequestValidator>();
        services.AddScoped<ChangeApprovalStatus.IRepository, ChangeApprovalStatus.Repository>();
        services.AddScoped<ChangeApprovalStatus.Service>();

        services.AddScoped<IValidator<Update.Request>, Update.RequestValidator>();
        services.AddScoped<Update.IRepository, Update.Repository>();
        services.AddScoped<Update.Service>();
    }
}
