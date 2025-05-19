namespace Kratos.Api.Features.Clients.GetById;

public static class Projections
{
    public record Client(
        long Id,
        long UserId,
        string FullName,
        string Email,
        string MobileNumber,
        string CloudStorageLink
    );
}
