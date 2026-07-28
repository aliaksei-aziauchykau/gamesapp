
namespace BGP.Shared.Libs.Common.Extensions;

public static class NullableExtensions
{
    public static T ThrowExceptionIfNull<T>(this T? value, string? message = null) where T : class
    {
        var messageToThrow = message ?? "Value cannot be null";

        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), messageToThrow);
        }

        return value;
    }
}
