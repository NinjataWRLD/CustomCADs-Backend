﻿using CustomCADs.Gallery.Domain.Carts.Entities;
using CustomCADs.Gallery.Domain.Common.Exceptions.CartItems;

namespace CustomCADs.Gallery.Domain.Carts.Validation;

using static CartConstants.CartItems;

public static class CartItemValidations
{
    public static CartItem ValidateQuantity(this CartItem item)
    {
        string property = "Quantity";
        int quantity = item.Quantity;

        int max = QuantityMax, min = QuantityMin;
        if (quantity > max || quantity < min)
        {
            throw CartItemValidationException.Range(property, max, min);
        }

        return item;
    }

    public static CartItem ValidatePriceAmount(this CartItem item)
    {
        string property = "Price Amount";
        decimal amount = item.Price.Amount;

        decimal max = PriceMax, min = PriceMin;
        if (amount > max || amount < min)
        {
            throw CartItemValidationException.Range(property, max, min);
        }

        return item;
    }
}