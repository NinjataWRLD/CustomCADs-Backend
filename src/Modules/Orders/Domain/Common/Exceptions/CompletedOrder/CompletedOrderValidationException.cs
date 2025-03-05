using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Bases.Exceptions;

namespace CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;

using static Constants.ExceptionMessages;

public class CompletedOrderValidationException : BaseException
{
    private CompletedOrderValidationException(string message, Exception? inner) : base(message, inner) { }

    public static CompletedOrderValidationException General(Exception? inner = default)
        => new(string.Format(Validation, "a", "Completed Order"), inner);

    public static CompletedOrderValidationException NotNull(string property, Exception? inner = default)
        => new(string.Format(NonNullValidation, "A", "Completed Order", property), inner);

    public static CompletedOrderValidationException Length(string property, int max, int min, Exception? inner = default)
        => new(string.Format(LengthValidation, "A", "Completed Order", property, min, max), inner);

    public static CompletedOrderValidationException Range(string property, decimal max, decimal min, Exception? inner = default)
        => new(string.Format(RangeValidation, "A", "Completed Order", property, min, max), inner);

    public static CompletedOrderValidationException OrderDateAfterPurchaseDate(DateTime orderDate, DateTime purchaseDate, Exception? inner = default)
        => new($"Order Date ({orderDate}) cannot be after Purchase Date ({purchaseDate}).", inner);

    public static CompletedOrderValidationException ShipmentIdOnNonDelivery(Exception? inner = default)
        => new("Cannot set a ShipmentId for a Completed Order without Delivery.", inner);
    
    public static CompletedOrderValidationException CustomizationIdOnNonDelivery(Exception? inner = default)
        => new("Cannot set a CustomizationId for a Completed Order without Delivery.", inner);

    public static CompletedOrderValidationException Custom(string message, Exception? inner = default)
        => new(message, inner);
}
