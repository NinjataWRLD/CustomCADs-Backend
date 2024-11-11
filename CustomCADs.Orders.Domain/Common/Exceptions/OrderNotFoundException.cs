using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions;

public class OrderNotFoundException : BaseException
{
    private OrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static OrderNotFoundException General(Exception? inner = default)
        => new("The requested Order does not exist.", inner);

    public static OrderNotFoundException ById(OrderId id, Exception? inner = default)
        => new($"The Order with id: {id} does not exist.", inner);

    public static OrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
