using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Domain.ActiveCarts.Exceptions.CartItems;

using static Constants.ExceptionMessages;

public class ActiveCartItemValidationException : BaseException
{
    private ActiveCartItemValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartItemValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "an", "Active Cart Item"), inner);

    public static ActiveCartItemValidationException Range<TType>(string property, TType max, TType min, Exception? inner = default) where TType : struct
        => new(string.Format(RangeValidation, "An", "Active Cart Item", property, min, max), inner);

    public static ActiveCartItemValidationException EditQuantityOnNonDelivery(ProductId id, Exception? inner = default)
        => new($"Cannot edit quantity of Active Cart Item: {id} because it is not for delivery", inner);

    public static ActiveCartItemValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
