using CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddView;

namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.Counts.AddView.Data;

public class AddProductViewValidData : AddProductViewData
{
    public AddProductViewValidData()
    {
        Add(1);
        Add(3);
        Add(5);
        Add(10);
    }
}
