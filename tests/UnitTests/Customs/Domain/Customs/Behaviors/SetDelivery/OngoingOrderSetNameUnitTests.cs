using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDelivery.Data;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDelivery;

public class CustomSetDeliveryUnitTests : CustomsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CustomSetDeliveryValidData))]
    public void SetDelivery_ShouldNotThrowException_WhenCustomValid(bool forDelivery)
    {
        CreateCustom().SetDelivery(forDelivery);
    }

    [Theory]
    [ClassData(typeof(CustomSetDeliveryValidData))]
    public void SetDelivery_ShouldPopulateProperly(bool forDelivery)
    {
        var Custom = CreateCustom();
        Custom.SetDelivery(forDelivery);
        Assert.Equal(forDelivery, Custom.ForDelivery);
    }

    [Theory]
    [ClassData(typeof(CustomSetDeliveryValidData))]
    public void SetDelivery_ShouldThrowException_WhenNameInvalid(bool forDelivery)
    {
        Assert.Throws<CustomValidationException<Custom>>(() =>
        {
            var custom = CreateCustom();
            custom.Report();
            custom.SetDelivery(forDelivery);
        });
    }
}
