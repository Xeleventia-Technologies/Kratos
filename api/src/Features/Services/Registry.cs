using FluentValidation;

using Kratos.Api.Common;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Services;

public class Registry : IRegistry
{
    public void MapEndpoints(WebApplication app)
    {
        app.MapPost("/service", Add.Handler.HandleAsync)
            .Accepts<IFormFile>(FormEncodingTypes.MultipartFormData)
            .DisableAntiforgery();
    }

    public void AddServices(IServiceCollection services)
    {
        services.AddScoped<IValidator<Add.Request>, Add.RequestValidator>();
        services.AddScoped<Add.Service>();
        services.AddScoped<Add.IRepository, Add.Repository>();
    }
}
