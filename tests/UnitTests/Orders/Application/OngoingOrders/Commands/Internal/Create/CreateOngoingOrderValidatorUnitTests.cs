using CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Create;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Internal.Create;

using static OngoingOrdersData;

public class CreateOngoingOrderValidatorUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly CreateOngoingOrderValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateOngoingOrderValidData))]
    public async Task Validate_ShouldBeValid_WhenOrderIsValid(string name, string description, bool delivery, AccountId buyerId)
    {
        // Arrange
        CreateOngoingOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateOngoingOrderInvalidNameData))]
    [ClassData(typeof(CreateOngoingOrderInvalidDescriptionData))]
    public async Task Validate_ShouldBeInvalid_WhenOrderIsNotValid(string name, string description, bool delivery, AccountId buyerId)
    {
        // Arrange
        CreateOngoingOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateOngoingOrderInvalidNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description, bool delivery, AccountId buyerId)
    {
        // Arrange
        CreateOngoingOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [ClassData(typeof(CreateOngoingOrderInvalidDescriptionData))]
    public async Task Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description, bool delivery, AccountId buyerId)
    {
        // Arrange
        CreateOngoingOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}
