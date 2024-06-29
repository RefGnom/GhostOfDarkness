namespace Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value)
    {
        if (value == null)
        {
            return true;
        }

        return value == "";
    }
}