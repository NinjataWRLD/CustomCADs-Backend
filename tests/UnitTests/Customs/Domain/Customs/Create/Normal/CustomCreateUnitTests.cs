namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;

using CustomCADs.Shared.Domain.Exceptions;
using Data;
using static CustomsData;

public class CustomCreateUnitTests : CustomsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CustomCreateValidData))]
	public void Create_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
	{
		CreateCustom(name, description, delivery, ValidBuyerId);
	}

	[Theory]
	[ClassData(typeof(CustomCreateValidData))]
	public void Create_ShouldPopulateProperties(string name, string description, bool forDelivery)
	{
		var order = CreateCustom(name, description, forDelivery, ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(name, order.Name),
			() => Assert.Equal(description, order.Description),
			() => Assert.Equal(forDelivery, order.ForDelivery),
			() => Assert.Equal(ValidBuyerId, order.BuyerId)
		);
	}

	[Theory]
	[ClassData(typeof(CustomCreateInvalidNameData))]
	[ClassData(typeof(CustomCreateInvalidDescriptionData))]
	public void Create_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool delivery)
	{
		Assert.Throws<CustomValidationException<Custom>>(
			() => CreateCustom(name, description, delivery, ValidBuyerId)
		);
	}
}
