using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Edit.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create;

using static CustomsData;

public class CreateCustomValidatorUnitTests : CustomsBaseUnitTests
{
	private readonly CreateCustomValidator validator = new();

	[Fact]
	public async Task Validate_ShouldBeValid_WhenCustomIsValid()
	{
		// Arrange
		CreateCustomCommand command = new(
			Name: MaxValidName,
			Description: MaxValidDescription,
			ForDelivery: true,
			BuyerId: ValidBuyerId
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(EditCustomInvalidData))]
	public async Task Validate_ShouldBeInvalid_WhenCustomIsNotValid(string name, string description, bool fordelivery)
	{
		// Arrange
		CreateCustomCommand command = new(
			Name: name,
			Description: description,
			ForDelivery: fordelivery,
			BuyerId: ValidBuyerId
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		Assert.False(result.IsValid);
	}
}
