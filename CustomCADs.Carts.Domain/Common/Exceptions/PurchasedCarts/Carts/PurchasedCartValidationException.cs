using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.Carts;

using static Constants.ExceptionMessages;

public class PurchasedCartValidationException : BaseException
{
    private PurchasedCartValidationException(string message, Exception? inner) : base(message, inner) { }

    public static PurchasedCartValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Purchased Cart"), inner);

    public static PurchasedCartValidationException Range(string property, int max, int min, Exception? inner = default)
        => new(string.Format(RangeValidation, "A", "Purchased Cart", property, min, max), inner);

    public static PurchasedCartValidationException ShipmentIdOnCartWithNoDelivery(Exception? inner = default)
        => new("Cannot set ShipmentId on a Purchased Cart with no requested Delivery", inner);

    public static PurchasedCartValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
