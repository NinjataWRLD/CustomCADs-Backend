using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;

using static Constants.ExceptionMessages;

public class ActiveCartItemValidationException : BaseException
{
    private ActiveCartItemValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartItemValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "an", "Active Cart Item"), inner);

    public static ActiveCartItemValidationException EditQuantityOnNonDelivery(ActiveCartItemId id, Exception? inner = default)
        => new($"Cannot edit quantity of Active Cart Item: {id} because it is not for delivery", inner);

    public static ActiveCartItemValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
