using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Application.Common.Exceptions;

public class CartItemDeliveryException : BaseException
{
    private CartItemDeliveryException(string message, Exception? inner) : base(message, inner) { }

    public static CartItemDeliveryException General(Exception? inner = null)
        => new("There was a mismatch between the attempted action and this Cart's Delivery flag.", inner);

    public static CartItemDeliveryException ById(CartId id, Exception? inner = null)
        => new($"There was a mismatch between the attempted action and Cart: {id}'s Delivery flag.", inner);

    public static CartItemDeliveryException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
