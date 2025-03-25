namespace CustomCADs.Orders.Domain.OngoingOrders;

using static OngoingOrderConstants;

public static class OngoingOrderValidations
{
    public static OngoingOrder ValidateName(this OngoingOrder order)
    {
        string property = "Name";
        string name = order.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw CustomValidationException<OngoingOrder>.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw CustomValidationException<OngoingOrder>.Length(property, minLength, maxLength);
        }

        return order;
    }

    public static OngoingOrder ValidateDescription(this OngoingOrder order)
    {
        string property = "Description";
        string description = order.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw CustomValidationException<OngoingOrder>.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw CustomValidationException<OngoingOrder>.Length(property, minLength, maxLength);
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
            throw CustomValidationException<OngoingOrder>.Range(property, max, min);
        }

        return order;
    }
}
