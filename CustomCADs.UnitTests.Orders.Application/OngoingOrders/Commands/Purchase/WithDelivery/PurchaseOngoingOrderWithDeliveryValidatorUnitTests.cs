using CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;

using static OngoingOrdersData;

public class PurchaseOngoingOrderWithDeliveryValidatorUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly PurchaseOngoingOrderWithDeliveryValidator validator = new();
    private static readonly OngoingOrderId id = OngoingOrderId.New();
    private static readonly AccountId buyerId = AccountId.New();

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidPaymentMethodIdData))]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidShipmentServiceData))]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidCountryData))]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidCityData))]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidPhoneData))]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidEmailData))]
    public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidPaymentMethodIdData))]
    public async Task Validate_ShouldReturnProperErrors_WhenPaymentMethodIdIsNotValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PaymentMethodId);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidShipmentServiceData))]
    public async Task Validate_ShouldReturnProperErrors_WhenShipmentServiceIsNotValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ShipmentService);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidCountryData))]
    public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.Country);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidCityData))]
    public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.City);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidPhoneData))]
    public async Task Validate_ShouldReturnProperErrors_WhenPhoneIsNotValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact.Phone);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderWithDeliveryInvalidEmailData))]
    public async Task Validate_ShouldReturnProperErrors_WhenEmailIsNotValid(string paymentMethodId, double weight, int count, string shipmentService, string country, string city, string? phone, string? email)
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId,
            Weight: weight,
            Count: count,
            ShipmentService: shipmentService,
            Address: new(country, city),
            Contact: new(phone, email)
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact.Email);
    }
}
