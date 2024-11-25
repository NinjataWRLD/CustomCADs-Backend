using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;
using CustomCADs.Shared.Core.Common.TypedIds.Shipments;

namespace CustomCADs.Delivery.Domain.Common.Exceptions.Shipments;

using static Constants.ExceptionMessages;

public class ShipmentNotFoundException : BaseException
{
    private ShipmentNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ShipmentNotFoundException General(Exception? inner = default)
        => new(string.Format(NotFound, "Shipment"), inner);

    public static ShipmentNotFoundException ById(ShipmentId id, Exception? inner = default)
        => new(string.Format(NotFoundByProp, "Shipment", nameof(id), id), inner);

    public static ShipmentNotFoundException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
