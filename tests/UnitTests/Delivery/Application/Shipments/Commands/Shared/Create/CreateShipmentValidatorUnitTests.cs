using CustomCADs.Delivery.Application.Shipments.Commands.Shared.Create;
using CustomCADs.Shared.UseCases.Shipments.Commands;
using CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Commands.Shared.Create;

using static ShipmentsData;

public class CreateShipmentValidatorUnitTests : ShipmentsBaseUnitTests
{
    private readonly CreateShipmentValidator validator = new();

    [Theory]
    [ClassData(typeof(CreateShipmentValidData))]
    public void Validator_ShouldBeValid_WhenShipmentIsValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidServiceData))]
    [ClassData(typeof(CreateShipmentInvalidCountData))]
    [ClassData(typeof(CreateShipmentInvalidWeightData))]
    [ClassData(typeof(CreateShipmentInvalidRecipientData))]
    [ClassData(typeof(CreateShipmentInvalidCountryData))]
    [ClassData(typeof(CreateShipmentInvalidCityData))]
    [ClassData(typeof(CreateShipmentInvalidPhoneData))]
    [ClassData(typeof(CreateShipmentInvalidEmailData))]
    public void Validator_ShouldBeInvalid_WhenShipmentIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidServiceData))]
    public void Validator_ShouldBeInvalid_WhenServiceIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Service);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidCountData))]
    public void Validator_ShouldBeInvalid_WhenCountIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Info.Count);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidWeightData))]
    public void Validator_ShouldBeInvalid_WhenWeightIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Info.Weight);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidRecipientData))]
    public void Validator_ShouldBeInvalid_WhenRecipientIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Info.Recipient);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidCountryData))]
    public void Validator_ShouldBeInvalid_WhenCountryIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.Country);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidCityData))]
    public void Validator_ShouldBeInvalid_WhenCityIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Address.City);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidPhoneData))]
    public void Validator_ShouldBeInvalid_WhenPhoneIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact.Phone);
    }

    [Theory]
    [ClassData(typeof(CreateShipmentInvalidEmailData))]
    public void Validator_ShouldBeInvalid_WhenEmailIsNotValid(string service, int count, double weight, string recipient, string country, string city, string street, string? phone, string? email)
    {
        // Arrange
        CreateShipmentCommand command = new(
            Service: service,
            Info: new(count, weight, recipient),
            Address: new(country, city, street),
            Contact: new(phone, email),
            BuyerId: ValidBuyerId
        );

        // Act
        var result = validator.TestValidate(new(command));

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Contact.Email);
    }
}
