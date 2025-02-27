namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Create.Normal.Data;

using static ActiveCartsData;

public class ActiveCartCreateValidData : ActiveCartCreateData
{
    public ActiveCartCreateValidData()
    {
        Add(ValidBuyerId1);
        Add(ValidBuyerId2);
    }
}