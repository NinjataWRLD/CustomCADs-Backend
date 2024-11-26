using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.Orders;

using static Constants.ExceptionMessages;

public class OrderNotFoundException : BaseException
{
    private OrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static OrderNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Order"), inner);

    public static OrderNotFoundException ById(OrderId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Order", nameof(id), id), inner);

    public static OrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
