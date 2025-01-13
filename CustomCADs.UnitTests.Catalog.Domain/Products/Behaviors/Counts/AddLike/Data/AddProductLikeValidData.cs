using CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddLike;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddLike.Data;

public class AddProductLikeValidData : AddProductLikeData
{
    public AddProductLikeValidData()
    {
        Add(1);
        Add(3);
        Add(5);
        Add(10);
    }
}
