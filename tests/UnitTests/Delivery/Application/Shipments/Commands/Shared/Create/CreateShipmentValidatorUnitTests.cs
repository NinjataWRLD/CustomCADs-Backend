using CustomCADs.Delivery.Application.Shipments.Commands.Shared.Create;
using CustomCADs.Shared.UseCases.Shipments.Commands;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create;

using static ShipmentsData;

public class CreateShipmentValidatorUnitTests : ShipmentsBaseUnitTests
{
	private readonly CreateShipmentValidator validator = new();

	[Fact]
	public void Validator_ShouldBeValid_WhenShipmentIsValid()
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: ValidService,
			Info: new(MaxValidCount, MaxValidWeight, ValidRecipient),
			Address: new(ValidCountry, ValidCity, ValidStreet),
			Contact: new(ValidPhone, ValidEmail),
			BuyerId: ValidBuyerId
		);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(Data.CreateShipmentInvalidData))]
	public void Validator_ShouldBeInvalid_WhenShipmentIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		CreateShipmentCommand command = new(
			Service: service,
			Info: new(count, weight, recipient),
			Address: new(country, city, street),
			Contact: new(phone, email),
			BuyerId: ValidBuyerId
		);

		// Act
		var result = validator.TestValidate(new(command));

		// Assert
		Assert.False(result.IsValid);
	}
}
