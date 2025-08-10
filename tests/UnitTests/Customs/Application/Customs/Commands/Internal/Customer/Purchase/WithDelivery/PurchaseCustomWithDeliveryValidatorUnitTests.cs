using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

using CustomCADs.Shared.Core.Common.TypedIds.Printing;
using Data;

public class PurchaseCustomWithDeliveryValidatorUnitTests : CustomsBaseUnitTests
{
	private readonly PurchaseCustomWithDeliveryValidator validator = new();

	private static readonly CustomId id = CustomId.New();
	private static readonly AccountId buyerId = AccountId.New();
	private static readonly CustomizationId customizationId = CustomizationId.New();

	[Theory]
	[ClassData(typeof(PurchaseCustomWithDeliveryValidData))]
	public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			PaymentMethodId: paymentMethodId,
			BuyerId: buyerId,
			CustomizationId: customizationId,
			Count: count,
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
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidShipmentServiceData))]
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidCountryData))]
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidCityData))]
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidPhoneData))]
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidEmailData))]
	public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			PaymentMethodId: paymentMethodId,
			BuyerId: buyerId,
			CustomizationId: customizationId,
			Count: count,
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
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidShipmentServiceData))]
	public async Task Validate_ShouldReturnProperErrors_WhenShipmentServiceIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			PaymentMethodId: paymentMethodId,
			BuyerId: buyerId,
			CustomizationId: customizationId,
			Count: count,
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
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidCountryData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			PaymentMethodId: paymentMethodId,
			BuyerId: buyerId,
			CustomizationId: customizationId,
			Count: count,
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
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidCityData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			PaymentMethodId: paymentMethodId,
			BuyerId: buyerId,
			CustomizationId: customizationId,
			Count: count,
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
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidPhoneData))]
	public async Task Validate_ShouldReturnProperErrors_WhenPhoneIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			PaymentMethodId: paymentMethodId,
			BuyerId: buyerId,
			CustomizationId: customizationId,
			Count: count,
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
	[ClassData(typeof(PurchaseCustomWithDeliveryInvalidEmailData))]
	public async Task Validate_ShouldReturnProperErrors_WhenEmailIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: id,
			PaymentMethodId: paymentMethodId,
			BuyerId: buyerId,
			CustomizationId: customizationId,
			Count: count,
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
