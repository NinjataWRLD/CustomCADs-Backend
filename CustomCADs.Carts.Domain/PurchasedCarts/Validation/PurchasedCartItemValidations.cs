﻿using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.Carts;
using CustomCADs.Carts.Domain.Common.Exceptions.PurchasedCarts.CartItems;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;

namespace CustomCADs.Carts.Domain.PurchasedCarts.Validation;

using static PurchasedCartConstants.PurchasedCartItems;

public static class PurchasedCartItemValidations
{
    public static PurchasedCartItem ValidateQuantity(this PurchasedCartItem item)
    {
        string property = "Quantity";
        int quantity = item.Quantity;

        int max = QuantityMax, min = QuantityMin;
        if (quantity > max || quantity < min)
        {
            throw PurchasedCartItemValidationException.Range(property, max, min);
        }

        return item;
    }

    public static PurchasedCartItem ValidatePrice(this PurchasedCartItem item)
    {
        string property = "Price";
        decimal amount = item.Price;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw PurchasedCartItemValidationException.Range(property, max, min);
        }

        return item;
    }
}
