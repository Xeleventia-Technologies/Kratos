using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Feeback.Add;

public static class Mappings
{
    public static Feedback AsFeedback(this Request request) => new Feedback
    {
        OutOfFiveRating = request.OutOfFiveRating,
        Comment = request.Comment,
    };
}
