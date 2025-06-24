using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Core.Common.Exceptions.Application;

public class CustomCachingException<TEntity> : BaseException where TEntity : class
{
	private CustomCachingException(string message, Exception? inner) : base(message, inner) { }

	public static CustomCachingException<TEntity> ByKey(
		string key,
		Exception? inner = default
	)
		=> new($"The {typeof(TEntity).Name} cache entry: {key} has a value stored, but it's null!", inner);

	public static CustomCachingException<TEntity> Custom(string message, Exception? inner = default)
		=> new(message, inner);
}
