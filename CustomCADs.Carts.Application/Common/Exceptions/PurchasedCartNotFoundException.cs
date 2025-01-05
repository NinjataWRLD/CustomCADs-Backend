using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class PurchasedCartNotFoundException : BaseException
{
    private PurchasedCartNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static PurchasedCartNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Cart"), inner);

    public static PurchasedCartNotFoundException ById(PurchasedCartId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Cart", nameof(id), id), inner);

    public static PurchasedCartNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
