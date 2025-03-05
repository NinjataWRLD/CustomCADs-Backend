using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Customizations;

namespace CustomCADs.Carts.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class ActiveCartItemNotFoundException : BaseException
{
    private ActiveCartItemNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartItemNotFoundException General(Exception? inner = null)
        => new(string.Format(NotFound, "Cart Item"), inner);

    public static ActiveCartItemNotFoundException ByProductId(ProductId productId, Exception? inner = null)
        => new(string.Format(NotFoundByProp, "Cart Item", nameof(productId), productId), inner);
    
    public static ActiveCartItemNotFoundException ByCustomizationId(CustomizationId customizationId, Exception? inner = null)
        => new(string.Format(NotFoundByProp, "Cart Item", nameof(customizationId), customizationId), inner);

    public static ActiveCartItemNotFoundException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
