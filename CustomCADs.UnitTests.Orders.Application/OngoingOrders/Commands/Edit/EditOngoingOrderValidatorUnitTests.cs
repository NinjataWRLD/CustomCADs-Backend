using CustomCADs.Orders.Application.OngoingOrders.Commands.Edit;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Edit.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Edit;

using static OngoingOrdersData;

public class EditOngoingOrderValidatorUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly EditOngoingOrderValidator validator = new();

    private static readonly OngoingOrderId id = OngoingOrderId.New();
    private static readonly AccountId buyerId = AccountId.New();

    [Theory]
    [ClassData(typeof(EditOngoingOrderValidData))]
    public async Task Validate_ShouldBeValid_WhenOrderIsValid(string name, string description)
    {
        // Arrange
        EditOngoingOrderCommand command = new(
            Id: id,
            Name: name,
            Description: description,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditOngoingOrderInvalidNameData))]
    [ClassData(typeof(EditOngoingOrderInvalidDescriptionData))]
    public async Task Validate_ShouldBeInvalid_WhenOrderIsNotValid(string name, string description)
    {
        // Arrange
        EditOngoingOrderCommand command = new(
            Id: id,
            Name: name,
            Description: description,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(EditOngoingOrderInvalidNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description)
    {
        // Arrange
        EditOngoingOrderCommand command = new(
            Id: id,
            Name: name,
            Description: description,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [ClassData(typeof(EditOngoingOrderInvalidDescriptionData))]
    public async Task Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description)
    {
        // Arrange
        EditOngoingOrderCommand command = new(
            Id: id,
            Name: name,
            Description: description,
            BuyerId: buyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}
