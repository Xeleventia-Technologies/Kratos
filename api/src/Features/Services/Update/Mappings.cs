namespace Kratos.Api.Features.Services.Update;

public static class Mappings
{
    public static Database.Models.Service AsService(this Request request) => new()
    {
        Name = request.Name,
        Summary = request.Summary,
        Description = request.Description
    };
}
