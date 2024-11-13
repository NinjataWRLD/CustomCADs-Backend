using CustomCADs.Orders.Domain.Common.Exceptions.GalleryOrders.Items;

namespace CustomCADs.Orders.Domain.GalleryOrders.Entities;

using static GalleryOrderConstants.Items;

public static class GalleryOrderItemValidations
{
    public static GalleryOrderItem ValidateQuantity(this GalleryOrderItem item)
    {
        string property = "Quantity";
        int quantity = item.Quantity;

        int max = QuantityMax, min = QuantityMin;
        if (quantity > max || quantity < min)
        {
            throw GalleryOrderItemValidationException.Range(property, max, min);
        }

        return item;
    }

    public static GalleryOrderItem ValidatePriceAmount(this GalleryOrderItem item)
    {
        string property = "Price Amount";
        decimal amount = item.Price.Amount;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw GalleryOrderItemValidationException.Range(property, max, min);
        }

        return item;
    }
}
