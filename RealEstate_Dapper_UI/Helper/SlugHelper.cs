using System.Globalization;
using System.Text.RegularExpressions;

namespace RealEstate_Dapper_UI.Helpers
{
    public static class SlugHelper
    {
        public static string ToSlug(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "ilan";

            text = text.ToLower(new CultureInfo("tr-TR"))
                       .Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i")
                       .Replace("ö", "o").Replace("ş", "s").Replace("ü", "u")
                       .Replace("â", "a").Replace("î", "i").Replace("û", "u");

            text = Regex.Replace(text, @"[^a-z0-9\s-]", "");
            text = Regex.Replace(text, @"[\s-]+", "-").Trim('-');

            return string.IsNullOrEmpty(text) ? "ilan" : text;
        }
    }
}