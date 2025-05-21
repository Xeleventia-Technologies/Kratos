namespace Kratos.Api.Features.Auth.Login;

public record LoggedInUser(
    long Id,
    string Name,
    string Email,
    string? DisplayPictureFileName,
    string AccessToken,
    string RefreshToken,
    string SessionId
);


public record LoggedInWebUser(
    long Id,
    string Name,
    string Email,
    string? DisplayPictureFileName,
    string AccessToken
);
