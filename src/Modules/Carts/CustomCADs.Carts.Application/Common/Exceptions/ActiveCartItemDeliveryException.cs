using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Carts.Application.Common.Exceptions;

public class ActiveCartItemDeliveryException : BaseException
{
    private ActiveCartItemDeliveryException(string message, Exception? inner) : base(message, inner) { }

    public static ActiveCartItemDeliveryException General(Exception? inner = null)
        => new("There was a mismatch between the attempted action and this Cart's Delivery flag.", inner);

    public static ActiveCartItemDeliveryException ById(ActiveCartId id, Exception? inner = null)
        => new($"There was a mismatch between the attempted action and Cart: {id}'s Delivery flag.", inner);

    public static ActiveCartItemDeliveryException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
