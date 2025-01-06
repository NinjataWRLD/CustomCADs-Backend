using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;

using static Constants.ExceptionMessages;

public class ActiveCartItemNotFoundException : BaseException
{
    private ActiveCartItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartItemNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Active Cart Item"), inner);

    public static ActiveCartItemNotFoundException ById(ActiveCartItemId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Active Cart Item", nameof(id), id), inner);

    public static ActiveCartItemNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
