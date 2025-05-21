namespace Kratos.Api.Features.Auth.Login;

public static class Mappings
{
    public static LoggedInWebUser AsWebUser(this LoggedInUser user) => new LoggedInWebUser(
        user.Id,
        user.Name,
        user.Email,
        user.DisplayPictureFileName,
        user.AccessToken
    );
}
