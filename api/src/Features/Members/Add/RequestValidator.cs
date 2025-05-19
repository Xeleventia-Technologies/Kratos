using FluentValidation;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Members.Add;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty();

        RuleFor(x => x.Bio)
            .NotEmpty();

        RuleFor(x => x.Position)
            .NotEmpty();

        RuleFor(x => x.DisplayPicture)
            .NotEmpty()
            .Must(image => AllowedMedia.ImageTypes.Contains(image.ContentType))
                .WithMessage("Allowed image types are: " + AllowedMedia.ImageTypes.CommaSeparated());
    }
}
