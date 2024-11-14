using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.Shipments;

public class ShipmentValidationException : BaseException
{
    private ShipmentValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ShipmentValidationException General(Exception? inner = default)
        => new("There was a validation error while working with a Shipment.", inner);

    public static ShipmentValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
