namespace CustomCADs.UnitTests.Catalog.Domain.Products.Behaviors.SetDesignerId;

using static ProductsData;

public class SetProductDesignerIdUnitTests : ProductsBaseUnitTests
{
    [Fact]
    public void SetDesignerId_ShouldNotThrowException()
    {
        CreateProduct().SetDesignerId(ValidDesignerId);
    }

    [Fact]
    public void SetDesignerId_ShouldPopulateProperly()
    {
        var product = CreateProduct();
        product.SetDesignerId(ValidDesignerId);
        Assert.Equal(ValidDesignerId, product.DesignerId);
    }
}
