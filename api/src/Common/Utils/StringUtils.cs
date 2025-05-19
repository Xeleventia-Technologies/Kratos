using System.Globalization;

namespace Kratos.Api.Common.Utils;

public static class StringUtils
{
    public static string ToTitleCase(this string text) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
}
