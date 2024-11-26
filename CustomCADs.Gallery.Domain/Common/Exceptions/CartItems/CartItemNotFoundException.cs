using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Gallery.Domain.Common.Exceptions.CartItems;

using static Constants.ExceptionMessages;

public class CartItemNotFoundException : BaseException
{
    private CartItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CartItemNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Cart Item"), inner);
    public static CartItemNotFoundException ById(CartItemId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Cart Item", nameof(id), id), inner);

    public static CartItemNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
