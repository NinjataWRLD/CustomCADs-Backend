using CustomCADs.Orders.Domain.Common.Exceptions.CompletedOrder;

namespace CustomCADs.Orders.Domain.CompletedOrders.Validation;

using static CompletedOrderConstants;

public static class CompletedOrderValidations
{
    public static CompletedOrder ValidateName(this CompletedOrder order)
    {
        string property = "Name";
        string name = order.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw CompletedOrderValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw CompletedOrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }

    public static CompletedOrder ValidateDescription(this CompletedOrder order)
    {
        string property = "Description";
        string description = order.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw CompletedOrderValidationException.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw CompletedOrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }
}
