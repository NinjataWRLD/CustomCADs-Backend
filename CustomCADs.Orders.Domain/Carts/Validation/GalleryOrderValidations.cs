using CustomCADs.Orders.Domain.Carts.Entities;
using CustomCADs.Orders.Domain.Common.Exceptions.Carts.GalleryOrders;

namespace CustomCADs.Orders.Domain.Carts.Validation;

using static CartConstants.GalleryOrders;

public static class GalleryOrderValidations
{
    public static GalleryOrder ValidateQuantity(this GalleryOrder item)
    {
        string property = "Quantity";
        int quantity = item.Quantity;

        int max = QuantityMax, min = QuantityMin;
        if (quantity > max || quantity < min)
        {
            throw GalleryOrderValidationException.Range(property, max, min);
        }

        return item;
    }

    public static GalleryOrder ValidatePriceAmount(this GalleryOrder item)
    {
        string property = "Price Amount";
        decimal amount = item.Price.Amount;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw GalleryOrderValidationException.Range(property, max, min);
        }

        return item;
    }
}
