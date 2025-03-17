namespace Kratos.Api.Features.Services.GetById;

public static class Projections
{
    public class Service
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageFileName { get; set; } = null!;
    }
}
