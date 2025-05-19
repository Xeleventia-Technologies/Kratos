using Kratos.Api.Database.Models;

namespace Kratos.Api.Features.Articles.Update;

public static class Mappings
{
    public static void UpdateFrom(this Article article, Request request, string? imageFileName)
    {
        article.Title = request.Title;
        article.Summary = request.Summary;
        article.Content = request.Content;

        if (imageFileName is not null)
        {
            article.ImageFileName = imageFileName;
        }
    }    
}
