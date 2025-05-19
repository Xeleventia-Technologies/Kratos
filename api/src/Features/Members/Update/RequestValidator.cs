using FluentValidation;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Extensions;

namespace Kratos.Api.Features.Members.Update;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty();

        RuleFor(x => x.Bio)
            .NotEmpty();

        When(x => x.DisplayPicture is not null, () =>
        {
            RuleFor(x => x.DisplayPicture)
                .NotEmpty()
                .Must(image => AllowedMedia.ImageTypes.Contains(image!.ContentType))
                    .WithMessage("Allowed image types are: " + AllowedMedia.ImageTypes.CommaSeparated());
        });
    }
}
