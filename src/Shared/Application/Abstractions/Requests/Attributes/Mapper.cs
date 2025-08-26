namespace CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

internal static class Mapper
{
	internal static Cache.Expiration ToCacheExpiration(this ExpirationType type, TimeSpan? time)
		=> new(
			Absolute: type is ExpirationType.Absolute ? time : null,
			Sliding: type is ExpirationType.Sliding ? time : null
		);

	internal static TimeSpan? ToTimeSpan(this TimeType type, int value)
		=> value is < 0
			? null
			: type switch
			{
				TimeType.Day => TimeSpan.FromDays(value),
				TimeType.Hour => TimeSpan.FromHours(value),
				TimeType.Minute => TimeSpan.FromMinutes(value),
				TimeType.Second => TimeSpan.FromSeconds(value),
				TimeType.None => null,
				_ => null,
			};
}
