namespace eShop.API.UI.Extensions;

public static class StringExtensions
{
    public static string Truncate(this string value, int length)
    {
        if (value.Length <= length) return value;
        return value.Trim().Substring(0, length - 4).Trim() + " ...";
        //return value[(length - 4)..] + " ...";
    }
}
