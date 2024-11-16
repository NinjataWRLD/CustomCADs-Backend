using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;

public class CustomOrderStatusException : BaseException
{
    private CustomOrderStatusException(string message, Exception? inner) : base(message, inner) { }

    public static CustomOrderStatusException General(Exception? inner = default)
        => new("The provided Custom Order cannot perform the requested action.", inner);

    public static CustomOrderStatusException ById(CustomOrderId id, string status, Exception? inner = default)
        => new($"The Custom Order with id: {id} cannot have a status: {status}.", inner);

    public static CustomOrderStatusException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
