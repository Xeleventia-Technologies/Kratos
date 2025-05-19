using FluentValidation;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Services.Add;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Summary)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Image)
            .NotEmpty()
            .Must(image => AllowedMedia.ImageTypes.Contains(image.ContentType))
                .WithMessage("Allowed image types are: " + AllowedMedia.ImageTypes.CommaSeparated());

        RuleFor(x => x.ParentServiceId)
            .GreaterThan(0)
            .When(x => x.ParentServiceId is not null);
    }
}
