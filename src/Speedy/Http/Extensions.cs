using System.Globalization;

namespace CustomCADs.Speedy.Http;

internal static class Extensions
{
	internal static DateOnly ParseDate(this string date)
		 => DateOnly.Parse(date, CultureInfo.InvariantCulture);

	internal static DateTimeOffset ParseDateTime(this string dateTime)
		=> DateTimeOffset.Parse(dateTime, CultureInfo.InvariantCulture);
}
