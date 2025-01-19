using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Delivery.Domain.Common.Exceptions.Shipments;

using static Constants.ExceptionMessages;

public class ShipmentValidationException : BaseException
{
    private ShipmentValidationException(string message, Exception? inner) : base(message, inner) { }

    public static ShipmentValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Shipment"), inner);
    
    public static ShipmentValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Shipment", property), inner);

    public static ShipmentValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
