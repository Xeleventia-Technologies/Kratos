namespace Kratos.Api.Common.Utils;

public static class DateUtils
{
    public static DateTimeOffset WithOffset(this DateTime dateTime) => new(dateTime, TimeSpan.Zero);
}
