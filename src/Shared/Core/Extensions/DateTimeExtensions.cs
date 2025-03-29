namespace CustomCADs.Shared.Core.Extensions;

public static class DateTimeExtensions
{
    public static DateTimeOffset ToUserLocalTime(this DateTimeOffset dateTimeOffset, string? timeZone)
        => timeZone is null 
            ? dateTimeOffset.ToUniversalTime()
            : TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset, timeZone);
}
