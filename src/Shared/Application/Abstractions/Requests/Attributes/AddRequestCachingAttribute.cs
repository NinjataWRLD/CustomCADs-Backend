namespace CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AddRequestCachingAttribute(
	ExpirationType expiration = ExpirationType.Absolute,
	TimeType time = TimeType.None,
	int value = -1
) : Attribute
{
	public Cache.Expiration Expiration = expiration.ToCacheExpiration(time.ToTimeSpan(value));
}
