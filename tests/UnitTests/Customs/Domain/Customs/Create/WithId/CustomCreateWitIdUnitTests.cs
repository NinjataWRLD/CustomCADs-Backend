using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId.Data;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;

public class CustomCreateWitIdUnitTests : CustomsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CustomCreateWitIdValidData))]
    public void CreateWithId_ShouldNotThrowException_WhenCustomIsValid(CustomId id, string name, string description, bool delivery, AccountId buyerId)
    {
        CreateCustomWithId(id, name, description, delivery, buyerId);
    }

    [Theory]
    [ClassData(typeof(CustomCreateWitIdValidData))]
    public void CreateWithId_ShouldPopulateProperties(CustomId id, string name, string description, bool forDelivery, AccountId buyerId)
    {
        var order = CreateCustomWithId(id, name, description, forDelivery, buyerId);

        Assert.Multiple(
            () => Assert.Equal(id, order.Id),
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(forDelivery, order.ForDelivery),
            () => Assert.Equal(buyerId, order.BuyerId)
        );
    }

    [Theory]
    [ClassData(typeof(CustomCreateWitIdInvalidNameData))]
    [ClassData(typeof(CustomCreateWitIdInvalidDescriptionData))]
    public void CreateWithId_ShouldThrowException_WhenCustomIsInvalid(CustomId id, string name, string description, bool delivery, AccountId buyerId)
    {
        Assert.Throws<CustomValidationException<Custom>>(() =>
        {
            CreateCustomWithId(id, name, description, delivery, buyerId);
            CreateCustom(name, description, delivery, buyerId);
        });
    }
}
