namespace CustomCADs.Orders.Domain.CompletedOrders;

using static CompletedOrderConstants;

public static class CompletedOrderValidations
{
    public static CompletedOrder ValidateName(this CompletedOrder order)
    {
        string property = "Name";
        string name = order.Name;

        if (string.IsNullOrEmpty(name))
        {
            throw CustomValidationException<CompletedOrder>.NotNull(property);
        }

        int maxLength = NameMaxLength, minLength = NameMinLength;
        if (name.Length > maxLength || name.Length < minLength)
        {
            throw CustomValidationException<CompletedOrder>.Length(property, minLength, maxLength);
        }

        return order;
    }

    public static CompletedOrder ValidateDescription(this CompletedOrder order)
    {
        string property = "Description";
        string description = order.Description;

        if (string.IsNullOrEmpty(description))
        {
            throw CustomValidationException<CompletedOrder>.NotNull(property);
        }

        int maxLength = DescriptionMaxLength, minLength = DescriptionMinLength;
        if (description.Length > maxLength || description.Length < minLength)
        {
            throw CustomValidationException<CompletedOrder>.Length(property, minLength, maxLength);
        }

        return order;
    }

    public static CompletedOrder ValidatePrice(this CompletedOrder order)
    {
        string property = "Price";
        decimal price = order.Price;

        decimal max = PriceMax, min = PriceMin;
        if (price > max || price < min)
        {
            throw CustomValidationException<CompletedOrder>.Range(property, min, max);
        }

        return order;
    }

    public static CompletedOrder ValidateOrderDate(this CompletedOrder order)
    {
        if (order.OrderDate > order.PurchaseDate)
        {
            throw CustomValidationException<CompletedOrder>.Custom($"Order Date ({order.OrderDate}) cannot be after Purchase Date ({order.PurchaseDate}).");
        }

        return order;
    }
}
