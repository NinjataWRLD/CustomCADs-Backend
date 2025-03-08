using CustomCADs.Shared.Core.Common.TypedIds.Customizations;
using CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.SetForDelivery.Data;

namespace CustomCADs.UnitTests.Carts.Domain.ActiveCarts.Items.Behaviors.SetForDelivery;

public class ActiveCartItemSetForDeliveryUnitTests : ActiveCartItemsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(ActiveCartItemSetForDeliveryValidData))]
    public void SetForDelivery_ShouldNotThrow(CustomizationId customizationId)
    {
        CreateItem().SetForDelivery(customizationId);
    }

    [Theory]
    [ClassData(typeof(ActiveCartItemSetForDeliveryValidData))]
    public void SetForDelivery_ShouldPopulateProperly(CustomizationId customizationId)
    {
        var item = CreateItem();
        item.SetForDelivery(customizationId);
        Assert.Multiple(
            () => Assert.True(item.ForDelivery),
            () => Assert.Equal(customizationId, item.CustomizationId)
        );
    }
}
