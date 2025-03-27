namespace CustomCADs.Shared.Core.Extensions;

public static class DateTimeExtensions
{
    public static DateTimeOffset ToUserLocalTime(this DateTimeOffset dateTimeOffset, string timeZone)
        => TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset, timeZone);
}
