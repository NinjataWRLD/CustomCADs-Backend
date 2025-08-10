using CustomCADs.Shared.Domain.Bases.Exceptions;

namespace CustomCADs.Shared.Application.Exceptions;

public class CustomException(string message, Exception? inner = null) : BaseException(message, inner)
{
	public static CustomException NullProp<TEntity>(string property, Exception? inner = null)
		=> new($"Cannot perform this action on {typeof(TEntity).Name} as it has no {property}.", inner);

	public static CustomException Delivery<TEntity>(bool markedForDelivery = false, Exception? inner = null)
		=> new($"The {typeof(TEntity).Name} is{(markedForDelivery ? "" : " not")} marked for delivery.", inner);

	public static CustomException NotPaid<TEntity>(Exception? inner = null)
		=> new($"The {typeof(TEntity).Name} requires its payment transaction to have been completed!", inner);
}
