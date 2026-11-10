namespace PetStock.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>String helpers for routes.</summary>
public static class StringExtensions
{
    /// <summary>Converts "StockAlerts" into "stock-alerts".</summary>
    public static string ToKebabCase(this string text)
    {
        return new string(Convert(text.GetEnumerator()).ToArray());

        static IEnumerable<char> Convert(CharEnumerator e)
        {
            if (!e.MoveNext()) yield break;

            yield return char.ToLower(e.Current);

            while (e.MoveNext())
                if (char.IsUpper(e.Current))
                {
                    yield return '-';
                    yield return char.ToLower(e.Current);
                }
                else
                {
                    yield return e.Current;
                }
        }
    }
}