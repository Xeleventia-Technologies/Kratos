using FluentValidation.Results;

using Kratos.Api.Common.Types;

namespace Kratos.Api.Common.Extensions;

public static class ResultExtensions
{
    public static IResult AsHttpResponse(this Result result)
    {
        if (!result.IsSuccess)
            return result.Error.AsHttpError();

        return Results.Ok();
    }

    public static IResult AsHttpResponse<T>(this Result<T> result) where T : class
    {
        if (!result.IsSuccess)
            return result.Error.AsHttpError();

        return Results.Ok(result.Value);
    }

    public static IResult AsHttpError(this ValidationResult validationResult) => Results.ValidationProblem(validationResult.ToDictionary());

    public static IResult AsHttpError(this Error? error)
    {
        return error switch
        {
            UnauthorizedError err => Results.Problem(statusCode: StatusCodes.Status401Unauthorized, detail: err.Message),
            ConflictError err => Results.Problem(statusCode: StatusCodes.Status409Conflict, detail: err.Message),
            NotFoundError err => Results.Problem(statusCode: StatusCodes.Status404NotFound, detail: err.Message),
            CannotProcessError err => Results.Problem(statusCode: StatusCodes.Status422UnprocessableEntity, detail: err.Message),
            BadRequestError err => Results.Problem(statusCode: StatusCodes.Status400BadRequest, detail: err.Message),

            Error err => Results.Problem(err.Message),
            _ => Results.Problem()
        };
    }
}
