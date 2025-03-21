namespace Kratos.Api.Features.Clients.GetAll;

public static class Projections
{
    public class Client
    {
        public long Id { get; set; }
        public string CloudStorageLink { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}