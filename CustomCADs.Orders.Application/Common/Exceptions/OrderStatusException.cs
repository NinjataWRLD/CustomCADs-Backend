using CustomCADs.Orders.Domain.Orders.Enums;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Application.Common.Exceptions;

public class OrderStatusException : BaseException
{
    private OrderStatusException(string message, Exception? inner) : base(message, inner) { }

    public static OrderStatusException General(Exception? inner = default)
        => new("The Order is does not have the appropriate status for this action.", inner);

    public static OrderStatusException ById(OrderId id, OrderStatus status, Exception? inner = default)
        => new($"The Order with id: {id} must be '{status}' for this action.", inner);

    public static OrderStatusException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
