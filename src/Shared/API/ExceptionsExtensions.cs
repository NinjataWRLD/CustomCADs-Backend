namespace CustomCADs.Shared.API;

public static class ExceptionsExtensions
{
	public static bool IsType(this Exception ex, Type type)
		=> ex.GetType().IsGenericType && ex.GetType().GetGenericTypeDefinition() == type;
}
