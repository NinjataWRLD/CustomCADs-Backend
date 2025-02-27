using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class PurchasedCartItemNotFoundException : BaseException
{
    private PurchasedCartItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static PurchasedCartItemNotFoundException General(Exception? inner = null)
        => new(string.Format(NotFound, "Cart Item"), inner);

    public static PurchasedCartItemNotFoundException ById(ProductId productId, Exception? inner = null)
        => new(string.Format(NotFoundByProp, "Cart Item", nameof(productId), productId), inner);

    public static PurchasedCartItemNotFoundException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
