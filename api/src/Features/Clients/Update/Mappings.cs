using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Clients.Update;

public static class Mappings
{
    public static Client AsClient(this Request request) => new()
    {
        CloudStorageLink = request.CloudStorageLink
    };
}