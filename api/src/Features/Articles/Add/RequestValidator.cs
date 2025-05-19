using FluentValidation;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Articles.Add;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Summary)
            .NotEmpty();

        RuleFor(x => x.Content)
            .NotEmpty();

        When(x => x.Image is not null, () => 
        {
            RuleFor(x => x.Image)
                .Must(image => AllowedMedia.ImageTypes.Contains(image!.ContentType))
                    .WithMessage("Allowed image types are: " + AllowedMedia.ImageTypes.CommaSeparated());
        });
    }
}
