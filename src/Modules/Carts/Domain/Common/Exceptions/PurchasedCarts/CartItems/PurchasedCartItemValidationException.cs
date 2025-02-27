using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.CartItems;

using static Constants.ExceptionMessages;

public class PurchasedCartItemValidationException : BaseException
{
    private PurchasedCartItemValidationException(string message, Exception? inner) : base(message, inner) { }

    public static PurchasedCartItemValidationException General(Exception? inner = null)
        => new(string.Format(Validation, "a", "Purchased Cart Item"), inner);

    public static PurchasedCartItemValidationException Range<T>(string property, T min, T max, Exception? inner = null)
        => new(string.Format(RangeValidation, "a", "Purchased Cart Item", property, min, max), inner);

    public static PurchasedCartItemValidationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
