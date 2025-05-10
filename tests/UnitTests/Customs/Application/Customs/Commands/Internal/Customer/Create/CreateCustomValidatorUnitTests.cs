using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Create;
using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Create;

using static CustomsData;

public class CreateCustomValidatorUnitTests : CustomsBaseUnitTests
{
    private readonly CreateCustomValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateCustomValidData))]
    public async Task Validate_ShouldBeValid_WhenCustomIsValid(string name, string description, bool fordelivery)
    {
        // Arrange
        CreateCustomCommand command = new(
            Name: name,
            Description: description,
            ForDelivery: fordelivery,
            BuyerId: ValidBuyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateCustomInvalidNameData))]
    [ClassData(typeof(CreateCustomInvalidDescriptionData))]
    public async Task Validate_ShouldBeInvalid_WhenCustomIsNotValid(string name, string description, bool fordelivery)
    {
        // Arrange
        CreateCustomCommand command = new(
            Name: name,
            Description: description,
            ForDelivery: fordelivery,
            BuyerId: ValidBuyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateCustomInvalidNameData))]
    public async Task Validate_ShouldReturnProperErrors_WhenNameIsNotValid(string name, string description, bool fordelivery)
    {
        // Arrange
        CreateCustomCommand command = new(
            Name: name,
            Description: description,
            ForDelivery: fordelivery,
            BuyerId: ValidBuyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Theory]
    [ClassData(typeof(CreateCustomInvalidDescriptionData))]
    public async Task Validate_ShouldReturnProperErrors_WhenDescriptionIsNotValid(string name, string description, bool fordelivery)
    {
        // Arrange
        CreateCustomCommand command = new(
            Name: name,
            Description: description,
            ForDelivery: fordelivery,
            BuyerId: ValidBuyerId
        );

        // Act
        var result = await validator.TestValidateAsync(command, cancellationToken: ct);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
}
