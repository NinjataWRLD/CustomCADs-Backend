using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class PurchasedCartItemNotFoundException : BaseException
{
    private PurchasedCartItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static PurchasedCartItemNotFoundException General(Exception? inner = null)
        => new(string.Format(NotFound, "Cart"), inner);

    public static PurchasedCartItemNotFoundException ById(PurchasedCartItemId id, Exception? inner = null)
        => new(string.Format(NotFoundByProp, "Cart", nameof(id), id), inner);

    public static PurchasedCartItemNotFoundException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
