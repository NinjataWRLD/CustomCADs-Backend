using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Delivery.Application.Common.Exceptions;

public class ShipmentAuthorizationException : BaseException
{
    private ShipmentAuthorizationException(string message, Exception? inner) : base(message, inner) { }

    public static ShipmentAuthorizationException General(Exception? inner = null)
        => new("Cannot modify another Buyer's Shipment.", inner);
    
    public static ShipmentAuthorizationException ById(ShipmentId id, Exception? inner = null)
        => new($"Cannot modify another Buyer's Shipment: {id}.", inner);

    public static ShipmentAuthorizationException Custom(string message, Exception? inner = null)
        => new(message, inner);
}
