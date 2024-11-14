using CustomCADs.Orders.Domain.Common.Exceptions;

namespace CustomCADs.Orders.Domain.Orders;

using static OrderConstants;

public static class OrderValidations
{
    public static Order ValidateName(this Order order)
    {
        string property = "Name";
        string name = order.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw OrderValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw OrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }

    public static Order ValidateDescription(this Order order)
    {
        string property = "Description";
        string description = order.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw OrderValidationException.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw OrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }
}
