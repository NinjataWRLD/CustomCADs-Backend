using CustomCADs.Shared.Domain.Bases.Exceptions;

namespace CustomCADs.Shared.Application.Exceptions;

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
