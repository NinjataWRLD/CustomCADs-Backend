﻿namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create.Data;

using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Create;
using static ActiveCartsData.CartItemsData;

public class ActiveCartItemCreateValidData : ActiveCartItemCreateData
{
    public ActiveCartItemCreateValidData()
    {
        Add(ActiveCartsData.ValidId1, ValidProductId1, ValidWeight1, true);
        Add(ActiveCartsData.ValidId2, ValidProductId2, ValidWeight2, false);
    }
}
