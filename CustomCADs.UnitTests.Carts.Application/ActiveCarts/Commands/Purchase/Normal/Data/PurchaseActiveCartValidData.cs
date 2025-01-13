﻿namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Purchase.Normal.Data;

using static ActiveCartsData;

public class PurchaseActiveCartValidData : PurchaseActiveCartData
{
    public PurchaseActiveCartValidData()
    {
        Add("payment-method-id-1", ValidBuyerId1);
        Add("payment-method-id-2", ValidBuyerId2);
    }
}
