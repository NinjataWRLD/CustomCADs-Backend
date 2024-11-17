using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts;

namespace CustomCADs.Orders.Domain.Carts.Validation;

using static CartConstants;

public static class CartValidations
{
    public static Cart ValidateOrders(this Cart cart)
    {
        string property = "Items";
        IEnumerable<GalleryOrder> items = cart.Orders;

        int max = ItemsCountMax, min = ItemsCountMin;
        if (items.Count() > max || items.Count() < min)
        {
            throw CartValidationException.Range(property, max, min);
        }

        return cart;
    }
}
