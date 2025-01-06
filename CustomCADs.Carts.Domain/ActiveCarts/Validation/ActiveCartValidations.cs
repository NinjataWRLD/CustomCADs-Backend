using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.Carts;

namespace CustomCADs.Carts.Domain.ActiveCarts.Validation;

using static ActiveCartConstants;

public static class ActiveCartValidations
{
    public static ActiveCart ValidateItems(this ActiveCart cart)
    {
        string property = "Items";
        IEnumerable<ActiveCartItem> items = cart.Items;

        int max = ItemsCountMax, min = ItemsCountMin;
        if (items.Count() > max || items.Count() < min)
        {
            throw ActiveCartValidationException.Range(property, max, min);
        }

        return cart;
    }
}
