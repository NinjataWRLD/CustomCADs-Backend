using CustomCADs.Carts.Domain.ActiveCarts.Entities;

namespace CustomCADs.Carts.Domain.ActiveCarts;

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
            throw CustomValidationException<ActiveCart>.Range(property, max, min);
        }

        return cart;
    }
}
