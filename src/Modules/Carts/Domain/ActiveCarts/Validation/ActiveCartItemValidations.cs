﻿using CustomCADs.Carts.Domain.ActiveCarts.Entities;
using CustomCADs.Carts.Domain.Common.Exceptions.ActiveCarts.CartItems;

namespace CustomCADs.Carts.Domain.ActiveCarts.Validation;

using static ActiveCartConstants.ActiveCartItems;

public static class ActiveCartItemValidations
{
    public static ActiveCartItem ValidateQuantity(this ActiveCartItem item)
    {
        string property = "Quantity";
        int quantity = item.Quantity;

        int max = QuantityMax, min = QuantityMin;
        if (quantity > max || quantity < min)
        {
            throw ActiveCartItemValidationException.Range(property, max, min);
        }

        return item;
    }
}
