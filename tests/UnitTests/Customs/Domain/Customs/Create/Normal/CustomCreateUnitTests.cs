using CustomCADs.Shared.Core.Common.Exceptions.Domain;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal.Data;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;

public class CustomCreateUnitTests : CustomsBaseUnitTests
{
    [Theory]
    [ClassData(typeof(CustomCreateValidData))]
    public void Create_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery, AccountId buyerId)
    {
        CreateCustom(name, description, delivery, buyerId);
    }

    [Theory]
    [ClassData(typeof(CustomCreateValidData))]
    public void Create_ShouldPopulateProperties(string name, string description, bool forDelivery, AccountId buyerId)
    {
        var order = CreateCustom(name, description, forDelivery, buyerId);

        Assert.Multiple(
            () => Assert.Equal(name, order.Name),
            () => Assert.Equal(description, order.Description),
            () => Assert.Equal(forDelivery, order.ForDelivery),
            () => Assert.Equal(buyerId, order.BuyerId)
        );
    }

    [Theory]
    [ClassData(typeof(CustomCreateInvalidNameData))]
    [ClassData(typeof(CustomCreateInvalidDescriptionData))]
    public void Create_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool delivery, AccountId buyerId)
    {
        Assert.Throws<CustomValidationException<Custom>>(() =>
        {
            CreateCustom(name, description, delivery, buyerId);
        });
    }
}
