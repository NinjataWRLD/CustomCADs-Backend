using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Gallery.Application.Carts.Exceptions;

using static Constants.ExceptionMessages;

public class CartNotFoundException : BaseException
{
    private CartNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CartNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Cart"), inner);

    public static CartNotFoundException ById(CartId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Cart", nameof(id), id), inner);

    public static CartNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
