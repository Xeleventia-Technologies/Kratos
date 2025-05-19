namespace Kratos.Api.Common.Types;

#region Do not touch

public enum SuccessStatus
{
    Ok,
    Created
}

public class ResultBase
{
    public bool IsSuccess { get; set; }
    public SuccessStatus? SuccessStatus { get; set; }
    public Error? Error { get; set; } = null!;
}

public class Result<T> : ResultBase
{
    public T Value { get; set; } = default!;

    public Result AsNonGeneric() => new()
    {
        IsSuccess = IsSuccess,
        SuccessStatus = SuccessStatus,
        Error = Error
    };

    public static implicit operator Result<T>(Result result) => new()
    {
        IsSuccess = result.IsSuccess,
        SuccessStatus = result.SuccessStatus,
        Error = result.Error
    };
}
#endregion

public class Result : ResultBase
{
    public static Result Success(SuccessStatus successStatus = Types.SuccessStatus.Ok) => new() { IsSuccess = true, SuccessStatus = successStatus };
    public static Result<T> Success<T>(T value, SuccessStatus successStatus = Types.SuccessStatus.Ok) => new() { IsSuccess = true, SuccessStatus = successStatus, Value = value };

    public static Result Fail(Error error) => new() { Error = error };

    public static Result Fail(string? message = null) => Fail(new Error(message));
    public static Result UnauthorizedError(string? message = null) => Fail(new UnauthorizedError(message));
    public static Result ConflictError(string? message = null) => Fail(new ConflictError(message));
    public static Result NotFoundError(string? message = null) => Fail(new NotFoundError(message));
    public static Result CannotProcessError(string? message = null) => Fail(new CannotProcessError(message));
    public static Result BadRequestError(string? message = null) => Fail(new BadRequestError(message));
}
