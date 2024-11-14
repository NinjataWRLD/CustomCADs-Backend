using CustomCADs.Cart.Domain.Common.Exceptions;

namespace CustomCADs.Cart.Domain.Carts.Entities;

using static CartConstants.Items;

public static class ItemValidations
{
    public static Item ValidateQuantity(this Item item)
    {
        string property = "Quantity";
        int quantity = item.Quantity;

        int max = QuantityMax, min = QuantityMin;
        if (quantity > max || quantity < min)
        {
            throw ItemValidationException.Range(property, max, min);
        }

        return item;
    }

    public static Item ValidatePriceAmount(this Item item)
    {
        string property = "Price Amount";
        decimal amount = item.Price.Amount;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw ItemValidationException.Range(property, max, min);
        }

        return item;
    }
}
