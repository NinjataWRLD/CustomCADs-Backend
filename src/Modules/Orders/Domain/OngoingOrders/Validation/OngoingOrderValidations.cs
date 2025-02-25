using CustomCADs.Orders.Domain.Common.Exceptions.OngoingOrders;

namespace CustomCADs.Orders.Domain.OngoingOrders.Validation;

using static OngoingOrderConstants;

public static class OngoingOrderValidations
{
    public static OngoingOrder ValidateName(this OngoingOrder order)
    {
        string property = "Name";
        string name = order.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw OngoingOrderValidationException.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw OngoingOrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }

    public static OngoingOrder ValidateDescription(this OngoingOrder order)
    {
        string property = "Description";
        string description = order.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw OngoingOrderValidationException.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw OngoingOrderValidationException.Length(property, maxLength, minLength);
        }

        return order;
    }

    public static OngoingOrder ValidatePrice(this OngoingOrder order)
    {
        string property = "Price";
        decimal? price = order.Price;

        decimal max = PriceMax, min = PriceMin;
        if (price > max || price < min)
        {
            throw OngoingOrderValidationException.Range(property, max, min);
        }

        return order;
    }
}
