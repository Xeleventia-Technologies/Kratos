using FluentValidation;

using Kratos.Api.Common.Constants;

namespace Kratos.Api.Features.Profiles.Update;

public class RequestValidator : AbstractValidator<Request>
{
    public RequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty();

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .MinimumLength(10)
                .WithMessage("Mobile number must be at least 10 digits")
            .MaximumLength(10)
                .WithMessage("Mobile number must be at most 10 digits")
            .Matches(@"^\d+$")
                .WithMessage("Mobile number must be a number");

        When(x => x.DisplayPicture is not null, () =>
        {
            RuleFor(x => x.DisplayPicture)
                .Must(image => AllowedMedia.ImageTypes.Contains(image!.ContentType))
                    .WithMessage("Allowed image types are: " + string.Join(", ", AllowedMedia.ImageTypes));
        });
    }
}
