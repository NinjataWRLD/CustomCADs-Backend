using CustomCADs.Shared.Core.Common.Exceptions;

namespace CustomCADs.Delivery.Domain.Common.Exceptions.Shipments;

public class ShipmentNotFoundException : BaseException
{
    private ShipmentNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ShipmentNotFoundException General(Exception? inner = default)
        => new("The requested Shipment does not exist.", inner);

    public static ShipmentNotFoundException ById(ShipmentId id, Exception? inner = default)
        => new($"The Shipment with id: {id} does not exist.", inner);

    public static ShipmentNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
