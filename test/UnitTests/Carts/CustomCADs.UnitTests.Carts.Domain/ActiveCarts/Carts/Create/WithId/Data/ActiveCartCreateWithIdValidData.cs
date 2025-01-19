namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Carts.Create.WithId.Data;

using static ActiveCartsData;

public class ActiveCartCreateWithIdValidData : ActiveCartCreateWithIdData
{
    public ActiveCartCreateWithIdValidData()
    {
        Add(ValidId1, ValidBuyerId1);
        Add(ValidId2, ValidBuyerId2);
    }
}