using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Purchase.Normal;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Purchase.Normal.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Purchase.Normal;

using static OngoingOrdersData;

public class PurchaseOngoingOrderValidatorUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly PurchaseOngoingOrderValidator validator = new();
    private static readonly OngoingOrderId id = OngoingOrderId.New();
    private static readonly AccountId buyerId = AccountId.New();

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId)
    {
        // Arrange
        PurchaseOngoingOrderCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderInvalidPaymentMethodIdData))]
    public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId)
    {
        // Arrange
        PurchaseOngoingOrderCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseOngoingOrderInvalidPaymentMethodIdData))]
    public async Task Validate_ShouldReturnProperErrors_WhenPaymentMethodIdIsNotValid(string paymentMethodId)
    {
        // Arrange
        PurchaseOngoingOrderCommand command = new(
            OrderId: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PaymentMethodId);
    }
}
