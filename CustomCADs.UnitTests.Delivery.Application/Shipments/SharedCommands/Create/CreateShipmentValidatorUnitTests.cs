namespace CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create;

using CustomCADs.Delivery.Application.Shipments.SharedCommands.Create;
using CustomCADs.Shared.UseCases.Shipments.Commands;
using CustomCADs.UnitTests.Delivery.Application.Shipments.SharedCommands.Create.Data;
using FluentValidation.TestHelper;
using static ShipmentsData;

public class CreateShipmentValidatorUnitTests : ShipmentsBaseUnitTests
{
    private readonly CreateShipmentValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerValidData))]
    public void Validator_ShouldBeValid_WhenShipmentIsValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidServiceData))]
    [ClassData(typeof(CreateShipmentHandlerInvalidCountData))]
    [ClassData(typeof(CreateShipmentHandlerInvalidWeightData))]
    [ClassData(typeof(CreateShipmentHandlerInvalidRecipientData))]
    [ClassData(typeof(CreateShipmentHandlerInvalidCountryData))]
    [ClassData(typeof(CreateShipmentHandlerInvalidCityData))]
    [ClassData(typeof(CreateShipmentHandlerInvalidPhoneData))]
    [ClassData(typeof(CreateShipmentHandlerInvalidEmailData))]
    public void Validator_ShouldBeInvalid_WhenShipmentIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidServiceData))]
    public void Validator_ShouldBeInvalid_WhenServiceIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Service);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidCountData))]
    public void Validator_ShouldBeInvalid_WhenCountIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Info.Count);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidWeightData))]
    public void Validator_ShouldBeInvalid_WhenWeightIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Info.Weight);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidRecipientData))]
    public void Validator_ShouldBeInvalid_WhenRecipientIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Info.Recipient);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidCountryData))]
    public void Validator_ShouldBeInvalid_WhenCountryIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.Country);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidCityData))]
    public void Validator_ShouldBeInvalid_WhenCityIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.City);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidPhoneData))]
    public void Validator_ShouldBeInvalid_WhenPhoneIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact.Phone);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentHandlerInvalidEmailData))]
    public void Validator_ShouldBeInvalid_WhenEmailIsNotValid(string service, int count, double weight, string recipient, string country, string city, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact.Email);
    }
}
