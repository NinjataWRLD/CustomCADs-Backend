using CustomCADs.Orders.Application.CompletedOrders.Commands.Create;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.Commands.Create;

using static CompletedOrdersData;

public class CreateCompletedOrderValidatorUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly CreateCompletedOrderValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateCompletedOrderValidData))]
    public async Task Validate_ShouldBeValid_WhenOrderIsValid(string name, string description, bool delivery, DateTime orderDate, AccountId buyerId, AccountId designerId, CadId cadId)
    {
        // Arrange
        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            OrderDate: orderDate,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderInvalidNameData))]
    [ClassData(typeof(CreateCompletedOrderInvalidDescriptionData))]
    public async Task Validate_ShouldBeInvalid_WhenOrderIsNotValid(string name, string description, bool delivery, DateTime orderDate, AccountId buyerId, AccountId designerId, CadId cadId)
    {
        // Arrange
        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            OrderDate: orderDate,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderInvalidNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description, bool delivery, DateTime orderDate, AccountId buyerId, AccountId designerId, CadId cadId)
    {
        // Arrange
        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            OrderDate: orderDate,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [ClassData(typeof(CreateCompletedOrderInvalidDescriptionData))]
    public async Task Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description, bool delivery, DateTime orderDate, AccountId buyerId, AccountId designerId, CadId cadId)
    {
        // Arrange
        CreateCompletedOrderCommand command = new(
            Name: name,
            Description: description,
            Delivery: delivery,
            OrderDate: orderDate,
            BuyerId: buyerId,
            DesignerId: designerId,
            CadId: cadId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}
