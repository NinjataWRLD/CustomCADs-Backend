using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Shared.Core.Common.Exceptions.Application;

public class CustomException(string message, Exception? inner = null) : BaseException(message, inner)
{
	public static CustomException NullProp<TEntity>(string property, Exception? inner = null)
		=> new($"Cannot perform this action on {typeof(TEntity).Name} as it has no {property}.", inner);

	public static CustomException Delivery<TEntity>(bool markedForDelivery = false, Exception? inner = null)
		=> new($"The {typeof(TEntity).Name} is {(markedForDelivery ? "" : "for")} marked for delivery.", inner);
}
