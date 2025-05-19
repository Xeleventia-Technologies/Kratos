using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Kratos.Api.Common.Extensions;

public static partial class StringExtensions
{
    public static string CommaSeparated(this string[] items) => string.Join(", ", items);
    public static string SeoFriendly(this string text, int maxLength = 100)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        // Normalize and remove diacritics
        text = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in text)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }
        text = sb.ToString().Normalize(NormalizationForm.FormC);

        // Convert to lower-case
        text = text.ToLowerInvariant();

        // Replace spaces with hyphens and remove invalid characters
        text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
        text = Regex.Replace(text, @"\s+", " ").Trim();
        text = text[..(text.Length <= maxLength ? text.Length : maxLength)].Trim();
        text = Regex.Replace(text, @"\s", "-");
        text = Regex.Replace(text, @"-+", "-");

        return text;
    }
}
