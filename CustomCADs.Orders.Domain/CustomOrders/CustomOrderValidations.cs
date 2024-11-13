using CustomCADs.Orders.Domain.Common.Exceptions.CustomOrders;

namespace CustomCADs.Orders.Domain.CustomOrders;

using static CustomOrderConstants;

public static class CustomOrderValidations
{
    public static CustomOrder ValidateName(this CustomOrder order)
    {
        string property = "Name";
        string name = order.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw CustomOrderValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw CustomOrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }

    public static CustomOrder ValidateDescription(this CustomOrder order)
    {
        string property = "Description";
        string description = order.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw CustomOrderValidationException.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw CustomOrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }
}
