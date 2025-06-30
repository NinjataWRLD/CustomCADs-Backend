using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

using Data;
using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryValidatorUnitTests : ActiveCartsBaseUnitTests
{
	private readonly PurchaseActiveCartWithDeliveryValidator validator = new();

	[Theory]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryValidData))]
	public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseActiveCartWithDeliveryCommand command = new(
			PaymentMethodId: paymentMethodId,
			BuyerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidShipmentServiceData))]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidCountryData))]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidCityData))]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidPhoneData))]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidEmailData))]
	public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseActiveCartWithDeliveryCommand command = new(
			PaymentMethodId: paymentMethodId,
			BuyerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command);

		// Assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidShipmentServiceData))]
	public async Task Validate_ShouldReturnProperErrors_WhenShipmentServiceIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseActiveCartWithDeliveryCommand command = new(
			PaymentMethodId: paymentMethodId,
			BuyerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ShipmentService);
	}

	[Theory]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidCountryData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseActiveCartWithDeliveryCommand command = new(
			PaymentMethodId: paymentMethodId,
			BuyerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.Country);
	}

	[Theory]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidCityData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseActiveCartWithDeliveryCommand command = new(
			PaymentMethodId: paymentMethodId,
			BuyerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.City);
	}

	[Theory]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidPhoneData))]
	public async Task Validate_ShouldReturnProperErrors_WhenPhoneIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseActiveCartWithDeliveryCommand command = new(
			PaymentMethodId: paymentMethodId,
			BuyerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Phone);
	}

	[Theory]
	[ClassData(typeof(PurchaseActiveCartWithDeliveryInvalidEmailData))]
	public async Task Validate_ShouldReturnProperErrors_WhenEmailIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseActiveCartWithDeliveryCommand command = new(
			PaymentMethodId: paymentMethodId,
			BuyerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Email);
	}
}
