using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Gallery.Domain.Carts.Validation;

using static CartConstants;

public static class CartValidations
{
    public static Cart ValidateItems(this Cart cart)
    {
        string property = "Items";
        IEnumerable<CartItem> items = cart.Items;

        int max = ItemsCountMax, min = ItemsCountMin;
        if (items.Count() > max || items.Count() < min)
        {
            throw CartValidationException.Range(property, max, min);
        }

        return cart;
    }
}
