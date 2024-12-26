using CustomCADs.Carts.Domain.Carts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.CartItems;

namespace CustomCADs.Carts.Domain.Carts.Validation;

using static CartConstants.CartItems;

public static class CartItemValidations
{
    public static CartItem ValidateQuantity(this CartItem item)
    {
        string property = "Quantity";
        int quantity = item.Quantity;

        int max = QuantityMax, min = QuantityMin;
        if (quantity > max || quantity < min)
        {
            throw CartItemValidationException.Range(property, max, min);
        }

        return item;
    }

    public static CartItem ValidateWeight(this CartItem item)
    {
        string property = "Weight";
        double quantity = item.Weight;

        double max = WeightMax, min = WeightMin;
        if (quantity > max || quantity < min)
        {
            throw CartItemValidationException.Range(property, max, min);
        }

        return item;
    }

    public static CartItem ValidatePrice(this CartItem item)
    {
        string property = "Price";
        decimal amount = item.Price;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw CartItemValidationException.Range(property, max, min);
        }

        return item;
    }
}
