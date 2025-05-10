using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

using Data;
using static ActiveCartsData;

public class PurchaseActiveCartValidatorUnitTests : ActiveCartsBaseUnitTests
{
    private readonly PurchaseActiveCartValidator validator = new();

    [Theory]
    [ClassData(typeof(PurchaseActiveCartValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId)
    {
        // Arrange
        PurchaseActiveCartCommand command = new(
            PaymentMethodId: paymentMethodId,
            BuyerId: ValidBuyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseActiveCartInvalidPaymentMethodIdData))]
    public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId)
    {
        // Arrange
        PurchaseActiveCartCommand command = new(
            PaymentMethodId: paymentMethodId,
            BuyerId: ValidBuyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseActiveCartInvalidPaymentMethodIdData))]
    public async Task Validate_ShouldReturnProperErrors_WhenPaymentMethodIdIsNotValid(string paymentMethodId)
    {
        // Arrange
        PurchaseActiveCartCommand command = new(
            PaymentMethodId: paymentMethodId,
            BuyerId: ValidBuyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PaymentMethodId);
    }
}
