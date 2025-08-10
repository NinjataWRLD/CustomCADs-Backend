using CustomCADs.Shared.Domain.Bases.Exceptions;

namespace CustomCADs.Shared.Application.Exceptions;

public class CustomAuthorizationException<TEntity> : BaseException where TEntity : class
{
	private CustomAuthorizationException(string message, Exception? inner) : base(message, inner) { }

	public static CustomAuthorizationException<TEntity> ById<TId>(
		TId id,
		Exception? inner = default
	) where TId : struct
		=> new($"Cannot access/modify another User's {typeof(TEntity).Name}: {id}.", inner);

	public static CustomAuthorizationException<TEntity> Custom(string message, Exception? inner = default)
		=> new(message, inner);
}
