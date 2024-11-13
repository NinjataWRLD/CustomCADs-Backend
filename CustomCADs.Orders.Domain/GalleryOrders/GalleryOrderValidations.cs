using CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders;
using CustomCADs.Orders.Domain.GalleryOrders.Entities;

namespace CustomCADs.Orders.Domain.GalleryOrders;

using static GalleryOrderConstants;

public static class GalleryOrderValidations
{
    public static GalleryOrder ValidateItems(this GalleryOrder cart)
    {
        string property = "Items";
        IEnumerable<GalleryOrderItem> items = cart.Items;

        int max = ItemsCountMax, min = ItemsCountMin;
        if (items.Count() > max || items.Count() < min)
        {
            throw GalleryOrderValidationException.Range(property, max, min);
        }

        return cart;
    }
}
