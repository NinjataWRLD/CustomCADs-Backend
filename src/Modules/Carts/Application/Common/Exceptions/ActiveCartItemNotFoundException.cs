using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Carts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class ActiveCartItemNotFoundException : BaseException
{
    private ActiveCartItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartItemNotFoundException General(Exception? inner = null)
        => new(string.Format(NotFound, "Cart"), inner);

    public static ActiveCartItemNotFoundException ById(ProductId productId, Exception? inner = null)
        => new(string.Format(NotFoundByProp, "Cart", nameof(productId), productId), inner);

    public static ActiveCartItemNotFoundException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
