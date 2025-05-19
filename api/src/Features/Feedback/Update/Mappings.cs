using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Feeback.Update;

public static class Mappings
{
    public static void UpdateFrom(this Feedback feedback, Request request)
    {
        feedback.OutOfFiveRating = request.OutOfFiveRating;
        feedback.Comment = request.Comment;
    }
}
