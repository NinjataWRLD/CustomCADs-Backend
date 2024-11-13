using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;

public class CustomOrderNotFoundException : BaseException
{
    private CustomOrderNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static CustomOrderNotFoundException General(Exception? inner = default)
        => new("The requested Order does not exist.", inner);

    public static CustomOrderNotFoundException ById(CustomOrderId id, Exception? inner = default)
        => new($"The Order with id: {id} does not exist.", inner);

    public static CustomOrderNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
