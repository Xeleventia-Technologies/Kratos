namespace Kratos.Api.Features.Feeback.Update;

public class Request
{
    public int OutOfFiveRating { get; set; }
    public string Comment { get; set; } = null!;
}
