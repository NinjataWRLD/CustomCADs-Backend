using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.Carts;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;

namespace CustomCADs.Carts.Domain.PurchasedCarts.Validation;

using static PurchasedCartConstants;

public static class PurchasedCartValidations
{
    public static PurchasedCart ValidateItems(this PurchasedCart cart)
    {
        string property = "Items";
        IEnumerable<PurchasedCartItem> items = cart.Items;

        int max = ItemsCountMax, min = ItemsCountMin;
        if (items.Count() > max || items.Count() < min)
        {
            throw PurchasedCartValidationException.Range(property, max, min);
        }

        return cart;
    }
}
