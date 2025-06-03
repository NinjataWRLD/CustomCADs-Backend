using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;

using Data;
using static CustomsData;

public class CustomCreateWitIdUnitTests : CustomsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CustomCreateWitIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
    {
        CreateCustomWithId(ValidId, name, description, delivery, ValidBuyerId);
    }

    [Theory]
    [ClassData(typeof(CustomCreateWitIdValidData))]
    public void CreateWithId_ShouldPopulateProperties(string name, string description, bool forDelivery)
    {
        var order = CreateCustomWithId(ValidId, name, description, forDelivery, ValidBuyerId);

        Assert.Multiple(
            () => Assert.Equal(ValidId, order.Id),
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(forDelivery, order.ForDelivery),
            () => Assert.Equal(ValidBuyerId, order.BuyerId)
        );
    }

    [Theory]
    [ClassData(typeof(CustomCreateWitIdInvalidNameData))]
    [ClassData(typeof(CustomCreateWitIdInvalidDescriptionData))]
    public void CreateWithId_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool delivery)
    {
        Assert.Throws<CustomValidationException<Custom>>((Action)(() =>
        {
            CustomsBaseUnitTests.CreateCustomWithId(CustomsData.ValidId, name, description, (bool?)delivery, ValidBuyerId);
            CreateCustom(name, description, delivery, ValidBuyerId);
        }));
    }
}
