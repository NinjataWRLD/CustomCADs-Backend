using CustomCADs.Cart.Domain.Carts.Entities;
using CustomCADs.Cart.Domain.Common.Exceptions;

namespace CustomCADs.Cart.Domain.Carts;

using static CartConstants;

public static class CartValidations
{
    public static Cart ValidateItems(this Cart cart)
    {
        string property = "Items";
        IEnumerable<Item> items = cart.Items;

        int max = ItemsCountMax, min = ItemsCountMin;
        if (items.Count() > max || items.Count() < min)
        {
            throw CartValidationException.Range(property, max, min);
        }

        return cart;
    }
}
