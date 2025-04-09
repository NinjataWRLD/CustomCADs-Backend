using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.Normal.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.Normal;

using static CustomsData;

public class PurchaseCustomValidatorUnitTests : CustomsBaseUnitTests
{
    private readonly PurchaseCustomValidator validator = new();
    private static readonly CustomId id = CustomId.New();
    private static readonly AccountId buyerId = AccountId.New();

    [Theory]
    [ClassData(typeof(PurchaseCustomValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId)
    {
        // Arrange
        PurchaseCustomCommand command = new(
            Id: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseCustomInvalidPaymentMethodIdData))]
    public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId)
    {
        // Arrange
        PurchaseCustomCommand command = new(
            Id: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(PurchaseCustomInvalidPaymentMethodIdData))]
    public async Task Validate_ShouldReturnProperErrors_WhenPaymentMethodIdIsNotValid(string paymentMethodId)
    {
        // Arrange
        PurchaseCustomCommand command = new(
            Id: id,
            PaymentMethodId: paymentMethodId,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PaymentMethodId);
    }
}
