using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Delivery.Application.Common.Exceptions;

using static Constants.ExceptionMessages;

public class ShipmentNotFoundException : BaseException
{
    private ShipmentNotFoundException(string message, Exception? inner) : base(message, inner) { }

    public static ShipmentNotFoundException General(Exception? inner = null)
        => new(string.Format(NotFound, "Shipment"), inner);
    
    public static ShipmentNotFoundException ById(ShipmentId id, Exception? inner = null)
        => new(string.Format(NotFoundByProp, "Shipment", nameof(id), id), inner);

    public static ShipmentNotFoundException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
