using CustomCADs.Carts.Application.ActiveCarts.Commands.Item.Add;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Add.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Item.Add;

public class AddActiveCartItemValidatorUnitTests : ActiveCartsBaseUnitTests
{
    private readonly AddActiveCartItemValidator validator = new();

    [Theory]
    [ClassData(typeof(AddActiveCartValidData))]
    public async Task Validate_ShouldBeValid_WhenCartIsValid(AccountId buyerId, double weight, bool forDelivery, ProductId productId)
    {
        // Arrange
        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            Weight: weight,
            ForDelivery: forDelivery,
            ProductId: productId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(AddActiveCartInvalidWeightData))]
    public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(AccountId buyerId, double weight, bool forDelivery, ProductId productId)
    {
        // Arrange
        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            Weight: weight,
            ForDelivery: forDelivery,
            ProductId: productId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(AddActiveCartInvalidWeightData))]
    public async Task Validate_ShouldReturnProperErrors_WhenWeightIsNotValid(AccountId buyerId, double weight, bool forDelivery, ProductId productId)
    {
        // Arrange
        AddActiveCartItemCommand command = new(
            BuyerId: buyerId,
            Weight: weight,
            ForDelivery: forDelivery,
            ProductId: productId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Weight);
    }
}
