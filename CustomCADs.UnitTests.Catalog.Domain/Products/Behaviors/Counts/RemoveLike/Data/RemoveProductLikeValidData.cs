using CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.RemoveLike;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.RemoveLike.Data;

public class RemoveProductLikeValidData : RemoveProductLikeData
{
    public RemoveProductLikeValidData()
    {
        Add(1);
        Add(3);
        Add(5);
        Add(10);
    }
}
